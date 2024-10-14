using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace ACM.ViewComponents
{
    [ViewComponent(Name = "Notifications")]
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;

        public NotificationsViewComponent(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService,
            IStringLocalizer<SessionStringLocalizer> localizer)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _localizer = localizer;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            NotificationsViewModel response = new NotificationsViewModel();
            response.NotificationList = new List<NotificationsViewModelData>();

            try
            {
                UserHelperFunctions userHelper = new UserHelperFunctions()
                {
                    _context = _context,
                    _emailService = _emailService,
                    _securityOptions = _securityOptions,
                    _user = User as ClaimsPrincipal
                };
                userHelper.Populate();

                response.NotificationList = _context.UserInAppNotifications.Where(x => x.UserID == userHelper.loggedInUserID && x.IsRead == false).OrderByDescending(x => x.EditDateTime).Take(5).ToList().Select(x => new NotificationsViewModelData
                {
                    Subject = x.Subject,
                    Title = x.Title,
                    UserInAppNotificationID = x.UserInAppNotificationID,
                    IconClass = "fa fa-envelope",
                    IconBGColor = "bg-cyan",
                    TimeAgo = x.CreatedDateTime.Value.ToTimezoneFromUtc(User as ClaimsPrincipal).Humanize(false, DateTime.UtcNow.ToTimezoneFromUtc(User as ClaimsPrincipal), new CultureInfo((!string.IsNullOrEmpty(userHelper.cultureNameCode)) ? userHelper.cultureNameCode : "en-ZA"))
                }).ToList();

                response.TotalNtf = _context.UserInAppNotifications.Where(x => x.UserID == userHelper.loggedInUserID && x.IsRead == false).Count();

            }
            catch (Exception ex)
            { 

            }

            return View(response);
        }
    }
}
