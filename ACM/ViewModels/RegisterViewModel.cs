using ACM.Helpers.EmailServiceFactory;
using ACM.Models.AccountDataModelFactory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using App.TemplateParser;

namespace ACM.ViewModels
{
    public class RegisterViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal string _errorMessage;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Display Name / Nickname")]
        public string DisplayName { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Cellphone Number")]
        [Required]
        public string CellphoneNumber { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Identification Number")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Accept Terms and Conditions")]
        public bool AcceptTermsAndConditions { get; set; }
        [Display(Name = "Timezone")]
        public string SelectedTimezone { get; set; }
        public string SelectedCountry { get; set; } = Guid.Empty.ToString();
        public string SelectedProvince { get; set; } = Guid.Empty.ToString();
        public string SelectedLanguageCultureID { get; set; }
        public string SelectedRegistrationRoleType { get; set; }
        public string ReturnUrl { get; set; }

        public string OrganisationName { get; set; }
        public string OrganisationAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Meters { get; set; }
        public string UserRole { get; set; }

        public List<SelectListItem> Timezones { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Provinces { get; set; }
        public List<SelectListItem> LanguageCultureList { get; set; }
        public List<SelectListItem> RegistrationRoleType { get; set; }

        public async Task PopulateLists(HttpContext httpContext)
        {
            ListHeplerViewModel listHeplerViewModel = new ListHeplerViewModel()
            {
                CountryID = SelectedCountry,
                ProvinceID = SelectedProvince,
                _context = _context,
            };
            listHeplerViewModel.PopulateLists();

            Countries = listHeplerViewModel.CountriesList;
            Provinces = listHeplerViewModel.ProvincesList;
            Timezones = listHeplerViewModel.TimeZoneList;

            LanguageCultureList = (from t in _context.LanguageCultures
                                   orderby t.Description
                                   select new SelectListItem
                                   {
                                       Value = t.LanguageCultureID.ToString(),
                                       Text = t.Description
                                   }).ToList();

            

            if (string.IsNullOrEmpty(SelectedTimezone))
            {
                SelectedTimezone = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_DEFAULT_TIME_ZONE.ToString()).ConfigValue;
            }
        }

        internal async Task<bool> Register()
        {
            bool isNew = false;

            if (string.IsNullOrEmpty(SelectedCountry))
            {
                _errorMessage = "Please Select a Country";
                return false;
            }
            else if (string.IsNullOrEmpty(SelectedProvince) || SelectedProvince == Guid.Empty.ToString())
            {
                _errorMessage = "Please Select a Province";
                return false;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                _errorMessage = "Please enter a Name";
                return false;
            }
            else if (string.IsNullOrEmpty(Surname))
            {
                _errorMessage = "Please enter a Surname";
                return false;
            }
            else if (string.IsNullOrEmpty(EmailAddress))
            {
                _errorMessage = "Please enter a Email";
                return false;
            }
            else if (string.IsNullOrEmpty(CellphoneNumber))
            {
                _errorMessage = "Please enter a Cellphone number";
                return false;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                _errorMessage = "Please enter a Password";
                return false;
            }
            else if (Password != ConfirmPassword)
            {
                _errorMessage = "The password and Confirm Password must match";
                return false;
            }
            else if (AcceptTermsAndConditions == false)
            {
                _errorMessage = "You have to read and accept the terms and conditions in order to register";
                return false;
            }

            //Org
            //var org = _context.Organisations.FirstOrDefault(x => ((x.OrganisationName == OrganisationName && x.OrganisationName != null)));
            //if (org == null)
            //{
            //    org = new Organisation();
            //    org.OrganisationID = Guid.NewGuid();
            //    org.OrganisationName = org.OrganisationName;
            //    org.OrganisationAddress = org.OrganisationAddress;
            //    org.ContactNo = org.ContactNo;
            //    org.Latitude = org.Latitude;
            //    org.Longitude = org.Longitude;
            //    org.Meters = org.Meters;
            //    org.EditUserID = org.EditUserID;
            //    org.CreatedDateTime = DateTime.UtcNow;
            //    org.EditDateTime = null;

            //    _context.Add(org);
            //    await _context.SaveChangesAsync();
            //}


            var user = _context.Users.FirstOrDefault(x => ((x.EmailAddress == EmailAddress && x.EmailAddress != null)));
            if (user == null)
            {
                user = new User();
                //user.OrganisationID = org.OrganisationID;
                isNew = true;
                user.UserID = Guid.NewGuid();
                user.IsSuspended = false;
                user.LoginTries = 0;
                user.CreatedUserID = user.UserID;
                user.CreatedDateTime = DateTime.UtcNow;
                user.IsRemoved = false;
                user.AcceptTermsAndConditions = AcceptTermsAndConditions;

                user.Password = HashProvider.ComputeHash(Password, HashProvider.HashAlgorithmList.SHA256, _securityOptions.PasswordSalt);
            }
            else
            {
                _errorMessage = "The user email address already exists. Find the existing user first and edit their details";
                return false;
            }

            

            var country = _context.Countries.Where(x => x.CountryID == Guid.Parse(SelectedCountry)).FirstOrDefault();
            if (IDNumber.Count() < country.IDNumberValidationLength || IDNumber.Count() > country.IDNumberValidationLength)
            {
                _errorMessage = "The ID number is not a valid Length";
                return false;
            }
            //user.OrganisationID = org.OrganisationID;
            user.DisplayName = DisplayName;
            user.EmailAddress = EmailAddress;
            user.CellphoneNumber = CellphoneNumber;
            user.IsSuspended = false;
            user.LoginTries = 0;
            user.EditUserID = user.UserID;
            user.EditDateTime = DateTime.UtcNow;
            user.FirstName = FirstName;
            user.Surname = Surname;
            user.Timezone = SelectedTimezone;
            user.Title = Title;
            user.IDNumber = IDNumber;

            user.LanguageCultureID = _context.LanguageCultures.First(x => x.CultureNameCode == "en-ZA").LanguageCultureID;
            user.CountryID = _context.Countries.First(x => x.Description == "South Africa").CountryID;
            user.ProvinceID = _context.Provinces.First(x => x.ProvIsoCode == "za-GP").ProvinceID;
            user.Timezone = "South Africa Standard Time";
            //user.AcmAccessRoleID = Guid.Parse(SelectedRegistrationRoleType);

            if (isNew)
            {
                _context.Add(user);

                user.IsAdminApproved = false;

                //Add user role
                LinkUserRole link = new LinkUserRole();
                link.LinkUserRoleID = Guid.NewGuid();
                link.UserID = user.UserID;
                link.UserRoleID = _context.UserRoles.First(x => x.EventCode == PublicEnums.UserRoleList.ROLE_USER).UserRoleID;
                link.CreatedUserID = user.UserID;
                link.EditUserID = user.UserID;
                _context.Add(link);
            }
            else
            {
                _context.Update(user);
            }

            await _context.SaveChangesAsync();

            //Send Email verification link
            UserHelperFunctions userHelper = new UserHelperFunctions();
            userHelper._context = _context;
            userHelper._emailService = _emailService;
            userHelper._securityOptions = _securityOptions;

            await userHelper.SendEmailVerificationLink(user.EmailAddress);

            //Send approval required email
            var adminUsers = _context.Users.Where(x => x.IsRemoved == false && x.IsEmailVerified == true
                && _context.LinkUserRole.Include(k => k.UserRole).Any(j => j.UserID == x.UserID && j.UserRole.EventCode == PublicEnums.UserRoleList.ROLE_ADMINISTRATOR)).ToList();

            foreach (var adminUser in adminUsers)
            {
                var variables = new Dictionary<string, PropertyMetaData>
                    {
                        {"HostUrl", new PropertyMetaData {Type = typeof (string), Value = _securityOptions.WebsiteHostUrl}},
                        {"DisplayName", new PropertyMetaData {Type = typeof (string), Value = adminUser.DisplayName}},
                        {"Username", new PropertyMetaData {Type = typeof (string), Value = user.EmailAddress}}
                    };

                await _emailService.SendEmailAsync(new List<string>() { adminUser.EmailAddress }, "Registration approval required", PublicEnums.EmailTemplateList.NTF_REGISTRATION_APPROVAL_REQUIRED, variables, null);

                await HelperFunctions.AddUserNotification(_context, adminUser.UserID, "Registration approval required", $"Registration approval required for '{user.EmailAddress}'.",
                    _emailService.EmailBody, PublicEnums.UserNotificationAction.NONE, null, null, true);
            }

            return true;
        }
    }
}
