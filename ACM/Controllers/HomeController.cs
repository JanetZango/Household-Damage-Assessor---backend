using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACM.Helpers.EmailServiceFactory;
using Microsoft.Extensions.Options;
using ACM.ViewModels.UserMyNotificationsViewModel;
using Microsoft.Extensions.Localization;
using ACM.Helpers.Localization;
using ACM.ViewModels.Services.SystemConfigServiceFactory;
using ACM.SignalRHubs;
using Microsoft.AspNetCore.SignalR;
using ACM.ViewModels.HomePageViewModelFactory;

namespace ACM.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;
        private readonly ISystemConfigService _systemConfig;
        private readonly IHubContext<UIUpdateHub> _uiUpdateHub;

        public HomeController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService,
            IStringLocalizer<SessionStringLocalizer> localizer, ISystemConfigService systemConfig,
            IHubContext<UIUpdateHub> uiUpdateHub)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _localizer = localizer;
            _systemConfig = systemConfig;
            _uiUpdateHub = uiUpdateHub;
        }
        [AllowAnonymous]
        public async Task<IActionResult> PublicTermsAndConditions()
        {
            PublicTermsAndConditionsViewModel model = new PublicTermsAndConditionsViewModel();

            try
            {
                model._context = _context;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._systemConfig = _systemConfig;
                model.PopulateModel();
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.MasterDataController.TermsAndConditions", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            //Set message if redirected from save


            ViewData.Model = model;
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> PublicContactUS()
        {
            PublicContactUsViewModel model = new PublicContactUsViewModel();

            try
            {
                model._context = _context;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._systemConfig = _systemConfig;
                await model.PopulateModel();
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.MasterDataController.TermsAndConditions", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            //Set message if redirected from save


            ViewData.Model = model;
            return View();
        }

        public async Task<IActionResult> Index()
        {
            HomePageViewModel model = new HomePageViewModel();
            model._context = _context;
            model._emailService = _emailService;
            model._securityOptions = _securityOptions;
            model._user = User;

            try
            {
                await model.PopulateModel();

                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.HomeController.Index", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MyNotifications()
        {
            UserMyNotificationsListViewModel model = new UserMyNotificationsListViewModel();

            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;

                await model.PopulateList();
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.HomeController.MyNotifications", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            ViewData.Model = model;

            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<JsonResult> MyNotifications(UserMyNotificationsListViewModel model)
        {
            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;

                await model.PopulateList();

                return Json(new { result = true, data = model });
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.HomeController.MyNotifications", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            return Json(new { result = false, message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message] });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MyNotificationDetails(Guid ID)
        {
            try
            {
                UserMyNotificationDetailsViewModel model = new UserMyNotificationDetailsViewModel();
                model._context = _context;
                model._user = User;
                model.UserInAppNotificationID = ID;
                model._uiUpdateHub = _uiUpdateHub;
                await model.Populate();

                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CourseManagementController.MyNotificationDetails", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MyNotificationBody(Guid ID)
        {
            try
            {
                UserMyNotificationDetailsViewModel model = new UserMyNotificationDetailsViewModel();
                model._context = _context;
                model._user = User;
                model.UserInAppNotificationID = ID;
                model._uiUpdateHub = _uiUpdateHub;
                await model.Populate();

                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CourseManagementController.MyNotificationBody", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> MyNotificationsMarkRead()
        {
            try
            {
                UserMyNotificationDetailsViewModel model = new UserMyNotificationDetailsViewModel();
                model._context = _context;
                model._user = User;
                model._uiUpdateHub = _uiUpdateHub;
                await model.MarkAllRead();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.HomeController.MyNotificationsMarkRead", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            return Json(new { result = false, message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message] });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UserNotifications()
        {
            return ViewComponent("Notifications");
        }

        public async Task<IActionResult> PublicFAQ()
        {

            FAQDisplayViewModel model = new FAQDisplayViewModel();
            model._context = _context;
            model._emailService = _emailService;
            model._securityOptions = _securityOptions;
            model._user = User;

            try
            {
                await model.Populate();
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.HomeController.PublicFAQ", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            ViewData.Model = model;
            return View();
        }
    }
}
