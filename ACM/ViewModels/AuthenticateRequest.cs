using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels
{
    public class AuthenticateRequest
    {
        [Required]
        [Display(Name = "Email Address / Username")]
        public string EmailAddressUsername { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
