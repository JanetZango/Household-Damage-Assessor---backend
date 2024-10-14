using App.TemplateParser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using ACM.Helpers.EmailServiceFactory;
using ACM.Services.ClickatellServiceFactory;
using ACM.SignalRHubs;
using ACM.ViewModels.Services.SystemConfigServiceFactory;
using System.IO;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace ACM.Helpers
{
    public class BackgroundJobHelper
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal JwtIssuerOptions _jwtOptions;
        internal IEmailService _emailService;
        internal IWebHostEnvironment _env;
        internal IHubContext<UIUpdateHub> _uiUpdateHub;
        internal HttpClient _httpClient;
        internal FileStorageOptions _fileStorageOptions;
        internal IConfiguration _configuration;
        internal IDistributedCache _cache;

        internal ISystemConfigService _systemConfig;
        internal IClickatellService _clickatellService;

        public BackgroundJobHelper(AppDBContext context, IOptions<SecurityOptions> securityOptions,
            IOptions<JwtIssuerOptions> jwtOptions, IEmailService emailService, IWebHostEnvironment env,
            IHubContext<UIUpdateHub> uiUpdateHub, ISystemConfigService systemConfig, IClickatellService clickatellService, 
            IOptions<FileStorageOptions> fileStoarageOptions, IConfiguration configuration, IDistributedCache cache)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _jwtOptions = jwtOptions.Value;

            _emailService = emailService;
            _env = env;

            _fileStorageOptions = fileStoarageOptions.Value;

            _uiUpdateHub = uiUpdateHub;
            _systemConfig = systemConfig;
            _clickatellService = clickatellService;

            _configuration = configuration;
            _cache = cache;
        }

        public async Task CleanApplicationLog()
        {
            int cleanDays = int.Parse(_context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_CLEAN_APP_LOG_DAYS.ToString()).ConfigValue);

            var logs = _context.ApplicationLog.Where(x => x.LogDate < DateTime.Now.AddDays(cleanDays * -1)).ToList();

            if (logs != null && logs.Count() > 0)
            {
                foreach (var log in logs)
                {
                    _context.ApplicationLog.Remove(log);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteFolder(string path)
        {
            //Sleep a bit to give time for resources to release
            await Task.Delay(20000);

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public async Task DeleteFile(string path)
        {
            //Sleep a bit to give time for resources to release
            await Task.Delay(20000);

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task SendUpcomingEventReminder(CancellationToken token)
        {
            try
            {
                var nowDate = DateTime.UtcNow;

                //User own upcoming events in next 30 min
                var upcomingEvents = _context.CalendarEvents.Where(x => x.StartTime > nowDate && x.StartTime <= nowDate.AddMinutes(30) && x.EnableReminder == true).ToList();
                if (upcomingEvents.Count > 0)
                {
                    foreach (var upcomingEvent in upcomingEvents)
                    {
                        if (token.IsCancellationRequested == false)
                        {
                            var user = _context.Users.FirstOrDefault(x => x.UserID == upcomingEvent.UserID);
                            if (user != null)
                            {
                                var authService = new Helpers.AuthenticationService
                                {
                                    _context = _context,
                                    _securityOptions = _securityOptions,
                                };

                                var _user = authService.CreateIdentity(user, false);

                                var variables = new Dictionary<string, PropertyMetaData>
                                    {
                                        {"HostUrl", new PropertyMetaData {Type = typeof (string), Value = _securityOptions.WebsiteHostUrl}},
                                        {"Name", new PropertyMetaData {Type = typeof (string), Value = user.DisplayName}},
                                        {"Description", new PropertyMetaData {Type = typeof (string), Value = upcomingEvent.Description}},
                                        {"StartTime", new PropertyMetaData {Type = typeof (string), Value = upcomingEvent.StartTime.ToTimezoneFromUtc(_user).ToString("yyyy/MM/dd HH:mm")}},
                                        {"IsAllDay", new PropertyMetaData {Type = typeof (string), Value = (upcomingEvent.IsAllDay) ? "Yes" : "No"}},
                                    };

                                await _emailService.SendEmailAsync(new List<string>() { user.EmailAddress }, "Upcoming event reminder", PublicEnums.EmailTemplateList.NTF_UPCOMING_EVENT_REMINDER, variables, _user);

                                await HelperFunctions.AddUserNotification(_context, upcomingEvent.UserID, "Upcoming event reminder", upcomingEvent.Description, _emailService.EmailBody, PublicEnums.UserNotificationAction.NONE, _uiUpdateHub, null, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Helpers.BackgroundJobHelper.SendUpcomingEventReminder", ex.Message, null, ex);
            }
        }

        public async Task SendFormBuilderQuestionNotificationEmail(string formDefinitionDescription, DateTime editDateTime, 
            string questionDescription, Guid formInstanceID, string answerDescription, string websiteHostUrl, string toEmailAddress, Guid userID,
            string remarks, List<EmailAttachment> attachments)
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                        .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                        .Options;

            using (var context1 = new AppDBContext(options))
            {
                var authService = new AuthenticationService();
                authService._context = context1;
                authService._securityOptions = _securityOptions;
                authService._cache = _cache;

                var user = context1.Users.AsNoTracking().First(x => x.UserID == userID);
                var claimsPrincipal = authService.CreateIdentity(user, false);

                var emails = toEmailAddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var toemail in emails)
                {
                    var variables = new Dictionary<string, PropertyMetaData>
                        {
                            {"FormDefinitionName", new PropertyMetaData {Type = typeof (string), Value = formDefinitionDescription}},
                            {"QuestionName", new PropertyMetaData {Type = typeof (string), Value = questionDescription}},
                            {"AnswerName", new PropertyMetaData {Type = typeof (string), Value = answerDescription}},
                            {"HostUrl", new PropertyMetaData {Type = typeof (string), Value = websiteHostUrl}},
                            {"Remarks", new PropertyMetaData {Type = typeof (string), Value = remarks}},
                            {"FormLink", new PropertyMetaData {Type = typeof (string), Value = $"{websiteHostUrl}/Forms/FormInstanceView/{formInstanceID}"}},
                            {"CompletionDate", new PropertyMetaData {Type = typeof (string), Value = editDateTime.ToString("yyyy/MM/dd HH:mm")}},
                        };

                    await _emailService.SendEmailAsync(new List<string>() { toemail }, "Form Submission - Question notification", PublicEnums.EmailTemplateList.NTF_FORM_SUBMISSION_QUESTION_NOTIFICATION,
                        variables, claimsPrincipal, attachments: attachments);
                }
            }
        }


        public void WriteErrorFile(string line)
        {
            try
            {
                var section = _configuration.GetSection("FileStorage");
                string filepath = section["ParentFolder"];
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string acrhiveDir = filepath + @"Acrhive";
                string fileAcrhiveSubDir = acrhiveDir + @"\" + DateTime.Now.ToString("yyyy/MM/dd");
                // If directory does not exist, create it.
                if (!Directory.Exists(acrhiveDir))
                {
                    Directory.CreateDirectory(acrhiveDir);
                }
                // Create a sub directory
                if (!Directory.Exists(fileAcrhiveSubDir))
                {
                    Directory.CreateDirectory(fileAcrhiveSubDir);
                }

                filepath = fileAcrhiveSubDir + @"\" + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString();
                    sw.WriteLine("----------- Files Updated-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

    }
}
