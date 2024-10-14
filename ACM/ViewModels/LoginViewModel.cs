using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels
{
    public class LoginViewModel
    {       

        [Required]
        [Display(Name = "Email address")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public string _errorMessage { get; set; }
    }
}
