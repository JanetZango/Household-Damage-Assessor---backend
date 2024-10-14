using ACM.Helpers.EmailServiceFactory;
using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels
{
    public class ForgotPasswordViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal IEmailService _emailService;

        [Required]
        [Display(Name = "Email address")]
        public string Username { get; set; }

        internal async Task<bool> SendForgotPasswordLink()
        {
            bool returnValue = false;

            UserHelperFunctions userHelper = new UserHelperFunctions
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            returnValue = await userHelper.SendForgotPasswordLink(Username);

            return returnValue;
        }

        internal async Task<bool> SendEmailVerificationLink()
        {
            bool returnValue = false;

            UserHelperFunctions userHelper = new UserHelperFunctions
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };

            returnValue = await userHelper.SendEmailVerificationLink(Username);

            return returnValue;
        }
    }
}
