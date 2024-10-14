using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using PayFast;

namespace ACM.ViewModels.Wallet
{
    public class TopupWalletViewModel
    {
        internal AppDBContext _context;
        internal IEmailService _emailService;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal IStringLocalizer<SessionStringLocalizer> _localizer;
        internal string _errorMessage = "";

        public string SelectedWalletAmount { get; set; }
        public double CustomAmount { get; set; }

        public List<SelectListItem> WalletAmountsList { get; set; }

        internal async Task PopulateList()
        {
            WalletAmountsList = new List<SelectListItem>()
            {
                new SelectListItem(){ 
                    Text = "R 50",
                    Value = "50.00"
                },
                new SelectListItem(){
                    Text = "R 100",
                    Value = "100.00"
                },
                new SelectListItem(){
                    Text = "R 200",
                    Value = "200.00"
                },
                new SelectListItem(){
                    Text = "R 500",
                    Value = "500.00"
                },
                new SelectListItem(){
                    Text = "R 1000",
                    Value = "1000.00"
                },
                new SelectListItem(){
                    Text = "R 1500",
                    Value = "1500.00"
                },
                new SelectListItem(){
                    Text = _localizer[PublicEnums.LocalizationKeys.Custom_Amount].Value,
                    Value = "CUSTOM_AMOUNT"
                }
            };
        }

        internal async Task<string> GetPaymentUrl()
        {
            string redirectUrl = "/";

            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            string passPhrase = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_PASSPHRASE.ToString()).ConfigValue;
            string merchantId = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTID.ToString()).ConfigValue;
            string merchantKey = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_MERCHANTKEY.ToString()).ConfigValue;
            string processUrl = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_PAYFAST_PROCESSURL.ToString()).ConfigValue;

            var onceOffRequest = new PayFastRequest(passPhrase);

            string returnlink = _securityOptions.WebsiteHostUrl + "/Payments/PaymentCompleted/";
            string cancellink = _securityOptions.WebsiteHostUrl + "/Payments/TopupWallet?isCancelled=true";
            string notifylink = _securityOptions.WebsiteHostUrl + "/Payments/PaymentITN/";

            // Merchant Details
            onceOffRequest.merchant_id = merchantId;
            onceOffRequest.merchant_key = merchantKey;
            onceOffRequest.return_url = returnlink;
            onceOffRequest.cancel_url = cancellink;
            onceOffRequest.notify_url = notifylink;

            // Buyer Details
            onceOffRequest.email_address = userHelper.user.EmailAddress;

            // Transaction Details
            onceOffRequest.m_payment_id = Guid.NewGuid().ToString();
            onceOffRequest.custom_str1 = userHelper.loggedInUserID.ToString();

            if (SelectedWalletAmount != "CUSTOM_AMOUNT")
            {
                onceOffRequest.amount = double.Parse(SelectedWalletAmount);
            }
            else
            {
                onceOffRequest.amount = CustomAmount;
            }

            onceOffRequest.item_name = "ACM Wallet Topup";
            //onceOffRequest.item_description = "";

            // Transaction Options
            onceOffRequest.email_confirmation = false;
            onceOffRequest.confirmation_address = "";

            redirectUrl = $"{processUrl}{onceOffRequest.ToString()}";

            return redirectUrl;
        }
    }
}
