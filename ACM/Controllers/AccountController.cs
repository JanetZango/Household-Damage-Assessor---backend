using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.Services.ClickatellServiceFactory;
using ACM.ViewModels.UsersViewModelFactory;

namespace ACM.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;
        private readonly IClickatellService _clickatellService;
        private readonly IWebHostEnvironment _env;
        private readonly FileStorageOptions _fileStorageOptions;
        public AccountController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService,
            IOptions<JwtIssuerOptions> jwtOptions, IConfiguration configuration, IDistributedCache cache, IStringLocalizer<SessionStringLocalizer> localizer,
            IClickatellService clickatellService, IWebHostEnvironment env, IOptions<FileStorageOptions> fileStorageOptions)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _jwtOptions = jwtOptions.Value;
            _configuration = configuration;
            _cache = cache;
            _localizer = localizer;
            _clickatellService = clickatellService;
            _env = env;
            _fileStorageOptions = fileStorageOptions.Value;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            try
            {
                model.ReturnUrl = ReturnUrl;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.Login", ex.Message, User, ex);
            }
            ViewData.Model = model;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var authService = new Helpers.AuthenticationService
                    {
                        _context = _context,
                        _httpContext = HttpContext,
                        _securityOptions = _securityOptions,
                        _cache = _cache
                    };
                    var authenticationResult = await authService.SignIn(model.Username, model.Password);
                    if (authenticationResult.IsSuccess)
                    {
                        return RedirectToLocal(model.ReturnUrl);
                    }
                    ViewBag.Error = authenticationResult.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.Login", ex.Message, User, ex);
            }
            return View(model);
        }
        private JsonResult RedirectToLocal(object returnUrl)
        {
            throw new NotImplementedException();
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model._context = _context;
                    model._securityOptions = _securityOptions;
                    model._user = User;
                    model._emailService = _emailService;
                    if (await model.SendForgotPasswordLink())
                    {
                        return RedirectToAction("ForgotPasswordConfirmation");
                    }
                    else
                    {
                        ViewBag.Error = "Unable to send password reset link. Please verify Email address correct. Note: Usernames without an email must be reset by an Administrator.";
                    }
                }
                catch (Exception ex)
                {
                    HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.ForgotPassword", ex.Message, User, ex);
                }
            }

            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string T)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = User,
                _emailService = _emailService,
                T = T
            };
            try
            {
                if (!await model.ValidateToken())
                {
                    ViewBag.Error = "The supplied token is not valid.";
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.ResetPassword", ex.Message, User, ex);
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            model._context = _context;
            model._securityOptions = _securityOptions;
            model._user = User;
            model._emailService = _emailService;
            if (ModelState.IsValid)
            {
                try
                {
                    if (await model.ChangePassword())
                    {
                        return RedirectToAction("ResetPasswordConfirmation");
                    }
                    else
                    {
                        ViewBag.Error = "Unable to Reset Password";
                    }
                }
                catch (Exception ex)
                {
                    HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.ResetPassword", ex.Message, User, ex);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmailAddress(string T)
        {
            UserDetailsViewModel model = new UserDetailsViewModel
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = User,
                _emailService = _emailService,
                _localizer = _localizer
            };
            try
            {
                if (!await model.VerifyEmailAddress(T))
                {
                    ViewBag.Error = "The supplied token is not valid.";
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.VerifyEmailAddress", ex.Message, User, ex);
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailVerificationLink()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmailVerificationLink(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model._context = _context;
                    model._securityOptions = _securityOptions;
                    model._user = User;
                    model._emailService = _emailService;
                    if (await model.SendEmailVerificationLink())
                    {
                        return RedirectToAction("SendEmailVerificationLinkConfirmation");
                    }
                    else
                    {
                        ViewBag.Error = "Unable to send email verification link. Please verify Email address correct. Note: Usernames without an email must be reset by an Administrator.";
                    }
                }
                catch (Exception ex)
                {
                    HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.SendEmailVerificationLink", ex.Message, User, ex);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult SendEmailVerificationLinkConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Profile(bool Success = false)
        {
            UserDetailsViewModel model = new UserDetailsViewModel();
            try
            {
                Guid loggedInUserID = Guid.Parse(User.Claims.First(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").Value);
                model.UserID = loggedInUserID;
                model._context = _context;
                model._securityOptions = _securityOptions;
                model._localizer = _localizer;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                await model.PopulateUserDetails();
                await model.PopulateLists();
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.Profile", ex.Message, User, ex);
                ViewBag.Error = "An error occurred while loading profile details";
            }
            ViewData.Model = model;
            if (Success)
            {
                ViewBag.Success = _localizer[PublicEnums.LocalizationKeys.User_Profile_Update_Success];
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Profile(UserDetailsViewModel model)
        {
            Guid loggedInUserID = Guid.Parse(User.Claims.First(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").Value);
            try
            {
                model._context = _context;
                model._securityOptions = _securityOptions;
                model.UserID = loggedInUserID;
                model._user = User;
                model._context = _context;
                model._localizer = _localizer;
                model._env = _env;
                model._fileStorageOptions = _fileStorageOptions;

                bool Result = await model.UpdateUserProfile();
                if (Result)
                {
                    return RedirectToAction("Profile", new { Success = true });
                }
                else
                {
                    ViewBag.Error = model.errorMessage;
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.Profile", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.User_Profile_Update_Fail];
            }

            await model.PopulateLists();
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Register(string ReturnUrl)
        {
            RegisterViewModel model = new RegisterViewModel();
            model._context = _context;
            model._securityOptions = _securityOptions;
            model._emailService = _emailService;
            model._errorMessage = "";
            try
            {
                model.ReturnUrl = ReturnUrl;
                await model.PopulateLists(HttpContext);
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.Register", ex.Message, User, ex);
            }
            ViewData.Model = model;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                model._context = _context;
                model._securityOptions = _securityOptions;
                model._emailService = _emailService;
                model._errorMessage = "";

                if (ModelState.IsValid)
                {
                    if (await model.Register())
                    {
                        return RedirectToAction("RegisterConfirmation", new { model.ReturnUrl });
                    }
                    else
                    {
                        ViewBag.Error = model._errorMessage;
                    }
                }
                else
                {
                    ViewBag.Error = "Please Enter all Information ";
                }
                await model.PopulateLists(HttpContext);
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.AccountController.Register", ex.Message, User, ex);
            }

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult RegisterConfirmation(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LoadUserProfileImage(string PIN = "", int width = 0, int height = 0)
        {
            bool emptyFile = false;
            var content = new byte[0];
            string etag = "";

            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = User
            };
            userHelper.Populate();

            AzureStorageHelperFunctions helper = new AzureStorageHelperFunctions();
            helper._securityOptions = _securityOptions;
            helper._fileStorageOptions = _fileStorageOptions;
            helper._env = _env;

            if (!string.IsNullOrEmpty(PIN))
            {
                byte[] ImageBlob = await helper.DownloadBlob(PIN);

                if (width > 0 && height > 0 && ImageBlob != null)
                {
                    ImageBlob = HelperFunctions.ResizeImagePreportional(ImageBlob, width, height, 100).ToArray();
                }

                content = ImageBlob;
            }
            else
            {
                var user = _context.Users.FirstOrDefault(x => x.UserID == userHelper.loggedInUserID);
                if (user != null && !string.IsNullOrEmpty(user.ProfileImageName))
                {
                    byte[] ImageBlob = await helper.DownloadBlob(user.ProfileImageName);

                    if (width > 0 && height > 0 && ImageBlob != null)
                    {
                        ImageBlob = HelperFunctions.ResizeImagePreportional(ImageBlob, width, height, 100).ToArray();
                    }

                    content = ImageBlob;
                }
                else
                {
                    emptyFile = true;
                }
            }

            if (!emptyFile)
            {
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = userHelper.loggedInUserID.ToString() + ".png",
                    Inline = true,
                };

                Response.Headers["Cache-Control"] = $"public,max-age=14400";
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("ETag", etag);

                return File(content, "image/png");
            }
            else
            {
                return File("~/img/profile-photos/1.png", "image/png");
            }
        }
    }
}
