using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.ViewModels.CalendarViewModelFactory;
using ACM.ViewModels.Services.SystemConfigServiceFactory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Wangkanai.Detection.Services;

namespace ACM.Controllers
{
    public class CalendarController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IDetectionService _detectionService;
        private readonly ISystemConfigService _systemConfig;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;

        public CalendarController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService,
            IDetectionService detectionService, ISystemConfigService systemConfig, IStringLocalizer<SessionStringLocalizer> localizer)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _detectionService = detectionService;
            _systemConfig = systemConfig;
            _localizer = localizer;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Calendar()
        {
            try
            {
                CalendarViewModel model = new CalendarViewModel()
                {
                    _context = _context,
                    _emailService = _emailService,
                    _securityOptions = _securityOptions,
                    _systemConfig = _systemConfig,
                    _localizer = _localizer,
                    _user = User
                };

                await model.PopulateModel(false);

                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CalendarController.Calendar", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }

            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> ReloadCalendar(CalendarViewModel model)
        {
            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._systemConfig = _systemConfig;
                model._localizer = _localizer;
                model._user = User;

                await model.PopulateModel();

                return Json(new { Result = true, Data = model });
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CalendarController.Calendar", ex.Message, User, ex);
            }

            return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> UpdateCalendarEventType(CalendarViewModelEventType EventTypeItem)
        {
            try
            {
                CalendarViewModel model = new CalendarViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._systemConfig = _systemConfig;
                model._localizer = _localizer;
                model._user = User;

                var typeID = await model.UpdateCalendarEventType(EventTypeItem);

                if (string.IsNullOrEmpty(model._errorMessage))
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.Event_types_updated_successfully].Value, data = typeID });
                }
                else
                {
                    return Json(new { Result = false, Message = model._errorMessage });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CalendarController.UpdateCalendarEventType", ex.Message, User, ex);
            }

            return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> RemoveCalendarEventType(Guid CalendarEventTypeID)
        {
            try
            {
                CalendarViewModel model = new CalendarViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._systemConfig = _systemConfig;
                model._localizer = _localizer;
                model._user = User;

                await model.RemoveCalendarEventType(CalendarEventTypeID);

                if (string.IsNullOrEmpty(model._errorMessage))
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.Event_type_removed_successfully].Value });
                }
                else
                {
                    return Json(new { Result = false, Message = model._errorMessage });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CalendarController.RemoveCalendarEventType", ex.Message, User, ex);
            }

            return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> DeleteCalendarEvent(Guid CalendarEventID)
        {
            try
            {
                CalendarViewModel model = new CalendarViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._systemConfig = _systemConfig;
                model._localizer = _localizer;
                model._user = User;

                await model.RemoveCalendarEvent(CalendarEventID);

                if (string.IsNullOrEmpty(model._errorMessage))
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.Event_removed_successfully].Value });
                }
                else
                {
                    return Json(new { Result = false, Message = model._errorMessage });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CalendarController.DeleteCalendarEvent", ex.Message, User, ex);
            }

            return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> UpdateCalendarEvent(CalendarViewModelEvent Event)
        {
            try
            {
                CalendarViewModel model = new CalendarViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._systemConfig = _systemConfig;
                model._localizer = _localizer;
                model._user = User;

                var eventID = await model.UpdateCalendarEvent(Event);

                if (string.IsNullOrEmpty(model._errorMessage))
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.Event_updated_successfully].Value, data = eventID });
                }
                else
                {
                    return Json(new { Result = false, Message = model._errorMessage });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.CalendarController.UpdateCalendarEvent", ex.Message, User, ex);
            }

            return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
        }
    }
}
