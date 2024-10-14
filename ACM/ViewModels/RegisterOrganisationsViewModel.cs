using ACM.Helpers.EmailServiceFactory;
using ACM.Models.AccountDataModelFactory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using App.TemplateParser;

namespace ACM.ViewModels
{
    public class RegisterOrganisationsViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal string _errorMessage;

        public Guid OrganisationID { get; set; }
        [Required]
        [Display(Name = "Organisation Name")]
        public string OrganisationName { get; set; }
        [Required]
        [Display(Name = "Organisation Address")]
        public string OrganisationAddress { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }
        [Required]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        [Required]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        [Required]
        [Display(Name = "Meters")]
        public string Meters { get; set; }
        public Guid? CreatedbyUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }
}
