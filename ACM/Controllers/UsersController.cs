using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ACM.Helpers.EmailServiceFactory;
using ACM.ViewModels.UsersViewModelFactory;
using ACM.Helpers.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Hosting;

namespace ACM.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR)]
    public class UsersController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;
        private readonly IWebHostEnvironment _env;
        private readonly FileStorageOptions _fileStorageOptions;
        public UsersController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService, IStringLocalizer<SessionStringLocalizer> localizer,
            IWebHostEnvironment env, IOptions<FileStorageOptions> fileStorageOptions)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _localizer = localizer;
            _env = env;
            _fileStorageOptions = fileStorageOptions.Value;
        }

        public async Task<IActionResult> Index(bool Removed = false)
        {
            try
            {
                UsersListViewModel model = new UsersListViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                //Set search based on session
                SessionManager sessionManager = new SessionManager();
                sessionManager.session = HttpContext.Session;
                var searchParmsObj = sessionManager.SearchParameters;
                var serachParams = searchParmsObj.PageSearchData.FirstOrDefault(x => x.PageEventCode == PublicEnums.PageList.PAGE_USERS_INDEX.ToString());
                if (serachParams != null)
                {
                    dynamic searchObj = serachParams.SearchParms;
                    model.Pagination = JsonConvert.DeserializeObject<PaginationViewModel>(searchObj.Pagination.ToString());
                    model.SearchValue = searchObj.SearchValue;
                }
                await model.PopulateList();
                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.Index", ex.Message, User, ex);
            }
            if (Removed)
            {
                ViewBag.Success = _localizer[PublicEnums.LocalizationKeys.User_removed_successfully];
            }
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Index(UsersListViewModel model)
        {
            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                await model.PopulateList();

                //Save search for later
                SessionManager sessionManager = new SessionManager();
                sessionManager.session = HttpContext.Session;
                var searchParmsObj = sessionManager.SearchParameters;
                var serachParams = searchParmsObj.PageSearchData.FirstOrDefault(x => x.PageEventCode == PublicEnums.PageList.PAGE_USERS_INDEX.ToString());
                if (serachParams != null)
                {
                    searchParmsObj.PageSearchData.Remove(serachParams);
                }
                searchParmsObj.PageSearchData.Add(new SearchParamatersViewModelData()
                {
                    PageEventCode = PublicEnums.PageList.PAGE_USERS_INDEX.ToString(),
                    SearchParms = new
                    {
                        Pagination = JsonConvert.SerializeObject(model.Pagination),
                        SearchValue = model.SearchValue
                    }
                });
                sessionManager.SearchParameters = searchParmsObj;
                return Json(new { result = true, data = model });
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.Index", ex.Message, User, ex);
            }
            return Json(new { result = false, message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message] });
        }
        [HttpPost]
        public async Task<JsonResult> RemoveUser(Guid ID)
        {
            try
            {
                UserDetailsViewModel model = new UserDetailsViewModel();
                model._context = _context;
                model._user = User;
                model.UserID = ID;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                bool removed = await model.RemoveUser();
                if (removed)
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.User_removed_successfully].Value });
                }
                else
                {
                    return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Unable_to_remove_user].Value });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.RemoveUser", ex.Message, User, ex);
                return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
            }
        }
        public async Task<IActionResult> Details(Guid ID, bool Success = false)
        {
            try
            {
                UserDetailsViewModel model = new UserDetailsViewModel();
                model._context = _context;
                model.UserID = ID;
                model._user = User;
                model._securityOptions = _securityOptions;
                model._localizer = _localizer;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                await model.PopulateUserDetails();
                await model.PopulateLists();
                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.Details", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }
            //Set message if redirected from save
            if (Success)
            {
                ViewBag.Success = _localizer[PublicEnums.LocalizationKeys.User_account_updated_successfully];
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(UserDetailsViewModel model)
        {
            model._context = _context;
            model._securityOptions = _securityOptions;
            model._emailService = _emailService;
            model._user = User;
            model._localizer = _localizer;
            model._env = _env;
            model._fileStorageOptions = _fileStorageOptions;

            try
            {
                if (model.UserRoles.Any(x => x.Selected == true) == false)
                {
                    ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Validate_One_User_Role];
                    await model.PopulateLists();
                    return View(model);
                }
                else
                {
                    Guid userID = Guid.Empty;
                    userID = await model.Save();
                    if (!string.IsNullOrEmpty(model.errorMessage))
                    {
                        ViewBag.Error = model.errorMessage;
                        await model.PopulateLists();
                        return View(model);
                    }
                    if (userID != Guid.Empty)
                    {
                        if (!string.IsNullOrEmpty(model._tmpPassword))
                        {
                            TempData["tmpPassword"] = model._tmpPassword;
                        }
                        else
                        {
                            TempData["tmpPassword"] = null;
                        }
                        return RedirectToAction("Details", new { ID = userID, Success = true });
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.Details", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }
            await model.PopulateLists();
            return View(model);
        }

        public async Task<IActionResult> ApprovalRequired(bool Approved = false, bool Declined = false)
        {
            try
            {
                UsersListViewModel model = new UsersListViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                //Set search based on session
                SessionManager sessionManager = new SessionManager();
                sessionManager.session = HttpContext.Session;
                var searchParmsObj = sessionManager.SearchParameters;
                var serachParams = searchParmsObj.PageSearchData.FirstOrDefault(x => x.PageEventCode == PublicEnums.PageList.PAGE_USERS_APPROVALREQUIRED.ToString());
                if (serachParams != null)
                {
                    dynamic searchObj = serachParams.SearchParms;
                    model.Pagination = JsonConvert.DeserializeObject<PaginationViewModel>(searchObj.Pagination.ToString());
                    model.SearchValue = searchObj.SearchValue;
                }

                await model.PopulateApprovalRequiredList();
                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.ApprovalRequired", ex.Message, User, ex);
            }
            if (Approved)
            {
                ViewBag.Success = _localizer[PublicEnums.LocalizationKeys.User_approved_successfully];
            }
            if (Declined)
            {
                ViewBag.Success = _localizer[PublicEnums.LocalizationKeys.User_declined_successfully];
            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ApprovalRequired(UsersListViewModel model)
        {
            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                await model.PopulateApprovalRequiredList();

                //Save search for later
                SessionManager sessionManager = new SessionManager();
                sessionManager.session = HttpContext.Session;
                var searchParmsObj = sessionManager.SearchParameters;
                var serachParams = searchParmsObj.PageSearchData.FirstOrDefault(x => x.PageEventCode == PublicEnums.PageList.PAGE_USERS_APPROVALREQUIRED.ToString());
                if (serachParams != null)
                {
                    searchParmsObj.PageSearchData.Remove(serachParams);
                }

                searchParmsObj.PageSearchData.Add(new SearchParamatersViewModelData()
                {
                    PageEventCode = PublicEnums.PageList.PAGE_USERS_APPROVALREQUIRED.ToString(),
                    SearchParms = new
                    {
                        Pagination = JsonConvert.SerializeObject(model.Pagination),
                        SearchValue = model.SearchValue
                    }
                });

                sessionManager.SearchParameters = searchParmsObj;
                return Json(new { result = true, data = model });
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.ApprovalRequired", ex.Message, User, ex);
            }

            return Json(new { result = false, message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message] });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveUser(Guid ID)
        {
            try
            {
                UserDetailsViewModel model = new UserDetailsViewModel();
                model._context = _context;
                model._user = User;
                model._securityOptions = _securityOptions;
                model._emailService = _emailService;
                model.UserID = ID;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                bool removed = await model.ApproveUser();
                if (removed)
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.User_approved_successfully].Value });
                }
                else
                {
                    return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Unable_to_approve_user].Value });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.ApproveUser", ex.Message, User, ex);
                return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeclineUser(Guid ID)
        {
            try
            {
                UserDetailsViewModel model = new UserDetailsViewModel();
                model._context = _context;
                model._user = User;
                model._securityOptions = _securityOptions;
                model._emailService = _emailService;
                model.UserID = ID;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                bool removed = await model.DeclineUser();
                if (removed)
                {
                    return Json(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.User_declined_successfully].Value });
                }
                else
                {
                    return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Unable_to_decline_user].Value });
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.DeclineUser", ex.Message, User, ex);
                return Json(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value });
            }
        }
    }
}
