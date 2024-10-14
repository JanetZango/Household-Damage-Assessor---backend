using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.ViewModels.Wallet;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PayFast;
using PayFast.AspNetCore;

namespace ACM.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;
        public PaymentsController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService, IStringLocalizer<SessionStringLocalizer> localizer)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _localizer = localizer;
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR + "," + PublicEnums.UserRoleList.ROLE_USER)]
        public async Task<IActionResult> TopupWallet(bool isCancelled = false)
        {
            try
            {
                TopupWalletViewModel model = new TopupWalletViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;

                await model.PopulateList();
                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.PaymentsController.TopupWallet", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value;
            }
            if(isCancelled)
            {
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Payment_Cancelled_Pleasee_Try_Again].Value;
            }

            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR + "," + PublicEnums.UserRoleList.ROLE_USER)]
        public async Task<IActionResult> PaymentCompleted()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR + "," + PublicEnums.UserRoleList.ROLE_USER)]
        [HttpPost]
        public async Task<IActionResult> TopupWallet(TopupWalletViewModel model)
        {
            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;

                var url = await model.GetPaymentUrl();
                if(string.IsNullOrEmpty(model._errorMessage))
                {
                    return Redirect(url);
                }
                else
                {
                    ViewBag.Error = model._errorMessage;
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.PaymentsController.TopupWallet", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value;
            }

            await model.PopulateList();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PaymentITN([ModelBinder(BinderType = typeof(PayFastNotifyModelBinder))] PayFastNotify payFastNotifyViewModel)
        {
            string passPhrase = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_PASSPHRASE.ToString()).ConfigValue;
            string merchantId = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTID.ToString()).ConfigValue;
            string merchantKey = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTKEY.ToString()).ConfigValue;
            string processUrl = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_PROCESSURL.ToString()).ConfigValue;
            string validateUrl = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_VALIDATEURL.ToString()).ConfigValue;

            payFastNotifyViewModel.SetPassPhrase(passPhrase);

            var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

            var isValid = payFastNotifyViewModel.signature == calculatedSignature;

            // The PayFast Validator is still under developement
            // Its not recommended to rely on this for production use cases
            var payfastValidator = new PayFastValidator(new PayFastSettings()
            {
                MerchantId = merchantId,
                MerchantKey = merchantKey,
                NotifyUrl = _securityOptions.WebsiteHostUrl + "/Payment/PaymentITN/",
                PassPhrase = passPhrase,
                ProcessUrl = processUrl,
                ValidateUrl = validateUrl,
            }, payFastNotifyViewModel, this.HttpContext.Connection.RemoteIpAddress);

            var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

            var ipAddressValidationResult = await payfastValidator.ValidateSourceIp();

            // Currently seems that the data validation only works for success
            if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
            {
                var dataValidationResult = await payfastValidator.ValidateData();

                if (dataValidationResult)
                {
                    string paymentID = payFastNotifyViewModel.m_payment_id;
                    string userID = payFastNotifyViewModel.custom_str1;

                    try
                    {
                        if (!string.IsNullOrEmpty(paymentID) && !string.IsNullOrEmpty(userID))
                        {
                            var user = _context.Users.FirstOrDefault(x => x.UserID == Guid.Parse(userID));
                            if(user != null)
                            {
                                _context.UserPaymentTransactions.Add(new Models.UserModelFactory.UserPaymentTransaction()
                                {
                                    AmountFee = payFastNotifyViewModel.amount_fee,
                                    AmountGross = payFastNotifyViewModel.amount_gross,
                                    AmountNet = payFastNotifyViewModel.amount_net,
                                    PaymentType = PublicEnums.PaymentTypeList.PAYMENT_GATEWAY.ToString(),
                                    TransactionDate = DateTime.UtcNow,
                                    TransactionType = PublicEnums.TransactionType.WALLET_TOPUP.ToString(),
                                    UserID = user.UserID,
                                    PFPaymentID = payFastNotifyViewModel.pf_payment_id,
                                    PFReferenceID = paymentID,
                                    PFPaymentStatus = payFastNotifyViewModel.payment_status,
                                    ItemName = payFastNotifyViewModel.item_name,
                                    UserPaymentTransactionID = Guid.NewGuid()
                                });

                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.PaymentsController.PaymentITN", ex.Message + "|paymentID:" + paymentID + "|UserID:" + userID, User, ex);
                    }
                }
            }

            if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
            {

            }

            return Ok();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR + "," + PublicEnums.UserRoleList.ROLE_USER)]
        public async Task<IActionResult> Transactions()
        {
            try
            {
                TransactionsViewModel model = new TransactionsViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;

                await model.PopulateList();
                ViewData.Model = model;
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.PaymentsController.Transactions", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value;
            }

            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = PublicEnums.UserRoleList.ROLE_ADMINISTRATOR + "," + PublicEnums.UserRoleList.ROLE_USER)]
        public async Task<JsonResult> Transactions(TransactionsViewModel model)
        {
            try
            {
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;
                model._localizer = _localizer;

                await model.PopulateList();

                return Json(new { result = true, data = model });
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.PaymentsController.Transactions", ex.Message, User, ex);
                ViewBag.Error = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value;
            }

            return Json(new { result = false, message = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message] });
        }
    }
}
