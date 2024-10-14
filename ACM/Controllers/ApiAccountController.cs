using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.Services.ClickatellServiceFactory;
using ACM.Models.SystemModelFactory;
using Microsoft.AspNetCore.Cors;
using ACM.Models.AccountDataModelFactory;
using App.TemplateParser;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Storage.Blobs.Models;
using System.Net.Mail;
using ACM.ViewModels.UsersViewModelFactory;

namespace ACM.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class ApiAccountController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;
        private readonly IClickatellService _clickatellService;

        public ApiAccountController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService,
            IOptions<JwtIssuerOptions> jwtOptions, IConfiguration configuration, IDistributedCache cache, IStringLocalizer<SessionStringLocalizer> localizer,
            IClickatellService clickatellService)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _jwtOptions = jwtOptions.Value;
            _configuration = configuration;
            _cache = cache;
            _localizer = localizer;
            _clickatellService = clickatellService;
        }

        [HttpGet]
        [Route("GetUsers")]
        [Produces(typeof(List<SelectList>))]
        [EnableCors]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                //UserHelperFunctions userHelper = new UserHelperFunctions()
                //{
                //    _context = _context,
                //    _securityOptions = _securityOptions,
                //    _user = User
                //};
                //userHelper.Populate();
                UsersListViewModel model = new UsersListViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;

                await model.PopulateList();

                return new JsonResult(model);
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiAccountController.GetAccessRoles", ex.Message, User, ex);
                return BadRequest(_localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel u)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => ((x.EmailAddress == u.EmailAddress && x.EmailAddress != null)));
                if (user == null)
                {
                    user = new User();
                    user.UserID = Guid.NewGuid();
                    user.IsSuspended = false;
                    user.LoginTries = 0;
                    user.CreatedUserID = user.UserID;
                    user.CreatedDateTime = DateTime.UtcNow;
                    user.IsRemoved = false;
                    user.AcceptTermsAndConditions = true;

                    user.Password = HashProvider.ComputeHash(u.Password, HashProvider.HashAlgorithmList.SHA256, _securityOptions.PasswordSalt);
                }
                else
                {
                    return Ok("The user email address already exists. Find the existing user first and edit their details");
                }


                //user.LanguageCultureID = _context.LanguageCultures.First(x => x.CultureNameCode == "en-ZA").LanguageCultureID;
                //user.CountryID = _context.Countries.First(x => x.Description == "South Africa").CountryID;
                //user.ProvinceID = _context.Provinces.First(x => x.ProvIsoCode == "za-GP").ProvinceID;
                user.Timezone = "South Africa Standard Time";


                user.DisplayName = u.FirstName +" "+ u.Surname;
                user.EmailAddress = u.EmailAddress;
                user.CellphoneNumber = u.CellphoneNumber;
                user.IsSuspended = false;
                user.LoginTries = 0;
                user.EditUserID = user.UserID;
                user.EditDateTime = DateTime.UtcNow;
                user.FirstName = u.FirstName;
                user.Surname = u.Surname;
                user.Title = "";
                user.IDNumber = u.IDNumber;
				user.IsEmailVerified = true;

				_context.Add(user);

                    user.IsAdminApproved = true;

                    //Add user role
                    LinkUserRole link = new LinkUserRole();
                    link.LinkUserRoleID = Guid.NewGuid();
                    link.UserID = user.UserID;
                    link.UserRoleID = _context.UserRoles.First(x => x.EventCode == PublicEnums.UserRoleList.ROLE_USER.ToString()).UserRoleID;
                    link.CreatedUserID = user.UserID;
                    link.EditUserID = user.UserID;
                    _context.Add(link);


                await _context.SaveChangesAsync();

                //Send Email verification link
                UserHelperFunctions userHelper = new UserHelperFunctions();
                userHelper._context = _context;
                userHelper._emailService = _emailService;
                userHelper._securityOptions = _securityOptions;

                await userHelper.SendEmailVerificationLink(user.EmailAddress);

                //Send approval required email
                var adminUsers = _context.Users.Where(x => x.IsRemoved == false && x.IsEmailVerified == true
                    && _context.LinkUserRole.Include(k => k.UserRole).Any(j => j.UserID == x.UserID && j.UserRole.EventCode == PublicEnums.UserRoleList.ROLE_ADMINISTRATOR)).ToList();

                foreach (var adminUser in adminUsers)
                {
                    var variables = new Dictionary<string, PropertyMetaData>
                    {
                        {"HostUrl", new PropertyMetaData {Type = typeof (string), Value = _securityOptions.WebsiteHostUrl}},
                        {"DisplayName", new PropertyMetaData {Type = typeof (string), Value = adminUser.DisplayName}},
                        {"Username", new PropertyMetaData {Type = typeof (string), Value = user.EmailAddress}}
                    };

                    await _emailService.SendEmailAsync(new List<string>() { adminUser.EmailAddress }, "Registration approval required", PublicEnums.EmailTemplateList.NTF_REGISTRATION_APPROVAL_REQUIRED, variables, null);

                    await HelperFunctions.AddUserNotification(_context, adminUser.UserID, "Registration approval required", $"Registration approval required for '{user.EmailAddress}'.",
                        _emailService.EmailBody, PublicEnums.UserNotificationAction.NONE, null, null, true);
                }

                return Ok("Successfully registered");

            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiAccountController.Profile", ex.Message, User, ex);
                return BadRequest(_localizer[PublicEnums.LocalizationKeys.User_Profile_Update_Fail].Value);
            }
        }


        [HttpGet]
        [Route("GetProfile")]
        [EnableCors]
        public async Task<IActionResult> GetProfile(string ID)
        {
            try
            {
                ProfileViewModel model = new ProfileViewModel();
                //UserHelperFunctions userHelper = new UserHelperFunctions()
                //{
                //    _context = _context,
                //    _securityOptions = _securityOptions,
                //    _user = User
                //};
                //userHelper.Populate();
                
                var user = _context.Users.FirstOrDefault(x => x.UserID == Guid.Parse(ID));
                if (user != null)
                {
                    model.UserID = user.UserID;
                   model.DisplayName = user.DisplayName;
                   model.EmailAddress = user.EmailAddress;
                   model.CellphoneNumber = user.CellphoneNumber;
                   model.Title = user.Title;
                   model.IDNumber = user.IDNumber;
                   model.Password = user.Password;
                   model.ConfirmPassword = user.Password;
                   model.UserRole = _context.LinkUserRole.Include(x => x.UserRole).FirstOrDefault(x => x.UserID == user.UserID).UserRole.EventCode;
                }

                return new JsonResult(model);



            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiAccountController.GetProfile", ex.Message, User, ex);
                return BadRequest(_localizer[PublicEnums.LocalizationKeys.User_Profile_Update_Fail].Value);
            }
        }
    }

    public class RegisterUserViewModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string IDNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CellphoneNumber { get; set; }
    }

    class ProfileViewModel
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;

        public Guid UserID { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }
        public string Title { get; set; }
        public string IDNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserRole { get; set; }
        
    }


}
