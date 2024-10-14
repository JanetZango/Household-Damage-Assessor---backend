using App.TemplateParser;
using Microsoft.AspNetCore.Mvc.Rendering;
using ACM.Helpers.EmailServiceFactory;
using ACM.Models.AccountDataModelFactory;
using System.ComponentModel.DataAnnotations;
using ACM.Helpers.Localization;
using Microsoft.Extensions.Localization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace ACM.ViewModels.UsersViewModelFactory
{
    public class UsersListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal IStringLocalizer<SessionStringLocalizer> _localizer;
        internal string errorMessage = "";
        internal IWebHostEnvironment _env;
        internal FileStorageOptions _fileStorageOptions;

        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public List<UsersListViewModelData> UserList { get; set; }
        internal async Task PopulateList()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }
            var list = (from u in _context.Users.Include( x => x.Province).ThenInclude(x => x.Country)
                        where u.IsRemoved == false && (!string.IsNullOrEmpty(SearchValue) && (u.DisplayName.Contains(SearchValue) || u.EmailAddress.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        select new UsersListViewModelData
                        {
                            AllowRemove = (userHelper.loggedInUserID == u.UserID) ? false : true,
                            DisplayName = u.DisplayName,
                            EmailAddress = u.EmailAddress,
                            IsSuspended = u.IsSuspended,
                            LoginTries = u.LoginTries,
                            CountryName = (u.Country != null) ? u.Country.Description : "",
                            ProvinceName = (u.Province != null) ? u.Province.Description : "",
                            UserID = u.UserID
                        });
            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            UserList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }

        internal async Task PopulateApprovalRequiredList()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }
            var list = (from u in _context.Users.Include(x => x.Province).ThenInclude(x => x.Country)
                        where u.IsRemoved == false && u.IsAdminApproved == false
                        && _context.LinkUserRole.Include(x => x.UserRole).Any(x => x.UserID == u.UserID && x.UserRole.EventCode == PublicEnums.UserRoleList.ROLE_ADMINISTRATOR) == false
                        && (!string.IsNullOrEmpty(SearchValue) && (u.DisplayName.Contains(SearchValue) || u.EmailAddress.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        select new UsersListViewModelData
                        {
                            AllowRemove = (userHelper.loggedInUserID == u.UserID) ? false : true,
                            DisplayName = u.DisplayName,
                            EmailAddress = u.EmailAddress,
                            IsSuspended = u.IsSuspended,
                            LoginTries = u.LoginTries,
                            CountryName = (u.Country != null) ? u.Country.Description : "",
                            ProvinceName = (u.Province != null) ? u.Province.Description : "",
                            UserID = u.UserID,
                        });
            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            UserList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
    public class UsersListViewModelData
    {
        public Guid UserID { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }
        public int LoginTries { get; set; }
        public bool IsSuspended { get; set; }
        public bool AllowRemove { get; set; }
        public string AccessRole { get; set; }
    }
    public class UserDetailsViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal string errorMessage = "";
        internal string _tmpPassword;
        internal IStringLocalizer<SessionStringLocalizer> _localizer;
        internal IWebHostEnvironment _env;
        internal FileStorageOptions _fileStorageOptions;

        #region Properties
        public Guid UserID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Display Name / Nickname")]
        public string DisplayName { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Cellphone Number")]
        public string CellphoneNumber { get; set; }
        [Display(Name = "Account Suspended")]
        public bool IsSuspended { get; set; }
        [Display(Name = "Timezone")]
        public string SelectedTimezone { get; set; }
        public string WebsiteHostUrl { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Identification Number")]
        public string IDNumber { get; set; }
        public string SelectedCountry { get; set; } = Guid.Empty.ToString();
        public string SelectedProvince { get; set; } = Guid.Empty.ToString();
        public string SelectedAcmAccessRole { get; set; } = Guid.Empty.ToString();
        public string ProvinceName { get; set; }
        [Display(Name = "Language - Culture")]
        public string SelectedLanguageCultureID { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool ReceiveEmailNotifactions { get; set; }
        public bool IsAdminApproved { get; set; }
        public string ProfileImageName { get; set; }
        public IFormFile ImageFile { get; set; }

        public List<UserDetailsViewModelRoles> UserRoles { get; set; }
        public List<SelectListItem> Timezones { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Provinces { get; set; }
        public List<SelectListItem> LanguageCultureList { get; set; }
        public List<SelectListItem> AcmAccessRoleList { get; set; }

        #endregion

        public async Task PopulateLists() // i do this to stop all Locations loading after save. it selects the correct location but other locations in other provinces also load. 
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

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
            LanguageCultureList = listHeplerViewModel.UserCultureList;
      
            //Populate user roles
            var allRoles = _context.UserRoles.ToList();
            UserRoles = (from r in allRoles
                         orderby r.Description
                         select new UserDetailsViewModelRoles
                         {
                             Description = r.Description,
                             EventCode = r.EventCode,
                             UserRoleID = r.UserRoleID,
                             Selected = CheckUserHasRole(r.UserRoleID)
                         }).ToList();

            AcmAccessRoleList.Insert(0, new SelectListItem()
            {
                Text = "None",
                Value = Guid.Empty.ToString()
            });

            //Populate timezone List

        }
        private bool CheckUserHasRole(Guid UserRoleID)
        {
            return _context.LinkUserRole.Any(l => l.UserRoleID == UserRoleID && l.UserID == UserID);
        }
        public Tuple<string,Guid> GetUserByEmail(string UserEmail)
        {
            var user = _context.Users.FirstOrDefault(x => x.EmailAddress == UserEmail.ToLower());
            var cellphoneNumberCountryID = new Tuple<string, Guid>(user.CellphoneNumber, (Guid)user.CountryID);
            return cellphoneNumberCountryID;
        }
        public async Task<bool> RemoveUser()
        {
            bool returnValue = false;
            var user = _context.Users.Where(x => x.UserID == UserID).FirstOrDefault();
            Guid loggedInUserID = Guid.Parse(_user.Claims.Where(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").First().Value);
            if (user != null)
            {
                //Remove user
                user.EditUserID = loggedInUserID;
                user.EditDateTime = DateTime.UtcNow;
                user.IsRemoved = true;

                if (!string.IsNullOrEmpty(user.ProfileImageName))
                {
                    AzureStorageHelperFunctions helper = new AzureStorageHelperFunctions();
                    helper._securityOptions = _securityOptions;
                    helper._fileStorageOptions = _fileStorageOptions;

                    await helper.DeleteBlob(user.ProfileImageName);
                }

                _context.Users.Update(user);

                user.Audit(_context, PublicEnums.DataAuditAction.ACTION_UPDATE);
                await _context.SaveChangesAsync();
                returnValue = true;
            }
            return returnValue;
        }
        private async Task ClearUserRoles(Guid userID)
        {
            Guid loggedInUserID = Guid.Parse(_user.Claims.Where(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").First().Value);
            var userRoles = _context.LinkUserRole.Where(x => x.UserID == userID);
            if (userRoles != null && userRoles.Count() > 0)
            {
                foreach (var linkRole in userRoles)
                {
                    linkRole.EditUserID = loggedInUserID;
                    linkRole.EditDateTime = DateTime.UtcNow;
                    _context.LinkUserRole.Remove(linkRole);
                    linkRole.Audit(_context, PublicEnums.DataAuditAction.ACTION_DELETE);
                }
            }
        }
        private async Task AddSelectedUserRoles(Guid userID)
        {
            Guid loggedInUserID = Guid.Parse(_user.Claims.Where(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").First().Value);
            foreach (var selectedRole in UserRoles.Where(x => x.Selected == true))
            {
                LinkUserRole link = new LinkUserRole();
                link.LinkUserRoleID = Guid.NewGuid();
                link.UserID = userID;
                link.UserRoleID = selectedRole.UserRoleID;
                link.CreatedUserID = loggedInUserID;
                link.EditUserID = loggedInUserID;
                link.EditDateTime = DateTime.UtcNow;
                link.CreatedDateTime = DateTime.UtcNow;
                _context.Add(link);
                link.Audit(_context, PublicEnums.DataAuditAction.ACTION_ADD);
            }
        }

        internal async Task<bool> ApproveUser()
        {
            bool returnValue = false;
            var user = _context.Users.Where(x => x.UserID == UserID).FirstOrDefault();
            Guid loggedInUserID = Guid.Parse(_user.Claims.Where(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").First().Value);
            if (user != null)
            {
                //Remove user
                user.EditUserID = loggedInUserID;
                user.EditDateTime = DateTime.UtcNow;
                user.IsAdminApproved = true;

                _context.Users.Update(user);
                user.Audit(_context, PublicEnums.DataAuditAction.ACTION_UPDATE);

                await _context.SaveChangesAsync();

                var variables = new Dictionary<string, PropertyMetaData>
                    {
                        {"HostUrl", new PropertyMetaData {Type = typeof (string), Value = _securityOptions.WebsiteHostUrl}},
                        {"DisplayName", new PropertyMetaData {Type = typeof (string), Value = user.DisplayName}},
                    };

                await _emailService.SendEmailAsync(new List<string>() { user.EmailAddress }, "Account Approved", PublicEnums.EmailTemplateList.NTF_ACCOUNT_APPROVED, variables, _user);

                await HelperFunctions.AddUserNotification(_context, user.UserID, "Account Approved", $"Your ACM user account has been approved",
                    _emailService.EmailBody, PublicEnums.UserNotificationAction.NONE, null, null, true);

                returnValue = true;
            }
            return returnValue;
        }

        internal async Task<bool> DeclineUser()
        {
            bool returnValue = false;
            var user = _context.Users.Where(x => x.UserID == UserID).FirstOrDefault();

            Guid loggedInUserID = Guid.Parse(_user.Claims.Where(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").First().Value);

            if (user != null)
            {
                //Remove roles
                var roles = _context.LinkUserRole.Where(x => x.UserID == UserID).ToList();
                _context.LinkUserRole.RemoveRange(roles);

                //Remove user
                user.EditUserID = loggedInUserID;
                user.EditDateTime = DateTime.UtcNow;

                _context.Users.Remove(user);

                user.Audit(_context, PublicEnums.DataAuditAction.ACTION_DELETE);
                await _context.SaveChangesAsync();

                returnValue = true;
            }

            return returnValue;
        }

        internal async Task<bool> VerifyEmailAddress(string T)
        {
            bool returnValue = false;
            if (!string.IsNullOrEmpty(T))
            {
                if (Guid.TryParse(T, out Guid tResult))
                {
                    var userToken = _context.UserTemporaryToken.Include(x => x.TemporaryTokensType).Where(x => x.UserTemporaryTokenID == tResult && x.TemporaryTokensType.EventCode == PublicEnums.TemporaryTokensTypeList.TYPE_EMAIL_VERIFICATION.ToString()).FirstOrDefault();
                    if (userToken != null)
                    {
                        if (userToken.TokenExpiryDate >= DateTime.Now)
                        {
                            var user = _context.Users.FirstOrDefault(x => x.UserID == userToken.UserID);
                            if (user != null)
                            {
                                user.IsEmailVerified = true;
                                user.EditDateTime = DateTime.UtcNow;
                                user.Audit(_context, PublicEnums.DataAuditAction.ACTION_UPDATE);
                                await _context.SaveChangesAsync();
                            }
                            returnValue = true;
                        }
                    }
                }
            }
            return returnValue;
        }

        public async Task PopulateUserDetails()
        {
            if (UserID != Guid.Empty)
            {
                var user = _context.Users.Where(x => x.UserID == UserID).FirstOrDefault();
                if (user != null)
                {
                    DisplayName = user.DisplayName;
                    EmailAddress = user.EmailAddress;
                    IsSuspended = user.IsSuspended;
                    FirstName = user.FirstName;
                    Surname = user.Surname;
                    SelectedTimezone = user.Timezone;
                    CellphoneNumber = user.CellphoneNumber;
                    Title = user.Title;
                    IDNumber = user.IDNumber;
                    SelectedCountry = user.CountryID.ToString();
                    SelectedProvince = user.ProvinceID.ToString();
                    SelectedLanguageCultureID = user.LanguageCultureID.ToString();
                    IsEmailVerified = user.IsEmailVerified;
                    ReceiveEmailNotifactions = user.ReceiveEmailNotification;
                    IsAdminApproved = user.IsAdminApproved;
                    ProfileImageName = user.ProfileImageName;
                }
            }
            else
            {
                SelectedTimezone = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_DEFAULT_TIME_ZONE.ToString()).ConfigValue;
                SelectedLanguageCultureID = "";
                IsSuspended = false;
                SelectedCountry = Guid.Empty.ToString();
                SelectedProvince = Guid.Empty.ToString();
                SelectedAcmAccessRole = Guid.Empty.ToString();
            }
            WebsiteHostUrl = _securityOptions.WebsiteHostUrl;
        }
        public async Task<Guid> Save()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();
           
            bool isNew = false;
            bool isWasRemoved = false;
            string tempPassword = "";
            PublicEnums.EmailTemplateList emailTemplate = PublicEnums.EmailTemplateList.NTF_REGISTRATION_WELCOME_CUSTOM;
            //Save user details
            var user = _context.Users.FirstOrDefault(x => x.UserID == UserID);
            if (user == null)
            {
                //Check user not deleted before
                user = _context.Users.FirstOrDefault(x => ((x.EmailAddress == EmailAddress && x.EmailAddress != null)) && x.IsRemoved == true);
                if (user == null)
                {
                    //Perform dup-check
                    user = _context.Users.FirstOrDefault(x => ((x.EmailAddress == EmailAddress && x.EmailAddress != null)) && x.IsRemoved == false);
                    if (user == null)
                    {
                        user = new User();
                        isNew = true;
                        user.UserID = Guid.NewGuid();
                        user.IsSuspended = false;
                        user.LoginTries = 0;
                        user.CreatedUserID = userHelper.loggedInUserID;
                        user.CreatedDateTime = DateTime.UtcNow;
                        user.IsRemoved = false;
                        tempPassword = HelperFunctions.GeneratePassword(8);
                        if (!string.IsNullOrEmpty(_tmpPassword))
                        {
                            tempPassword = _tmpPassword;
                        }
                        user.Password = HashProvider.ComputeHash(tempPassword, HashProvider.HashAlgorithmList.SHA256, _securityOptions.PasswordSalt);
                        _tmpPassword = tempPassword;
                    }
                    else
                    {
                        errorMessage = _localizer[PublicEnums.LocalizationKeys.Profile_Email_Dup_Error]; ; // "The user email address already exists. Find the existing user first and edit their details";
                        return Guid.Empty;
                    }
                }
                else
                {
                    tempPassword = HelperFunctions.GeneratePassword(8);
                    if (!string.IsNullOrEmpty(_tmpPassword))
                    {
                        tempPassword = _tmpPassword;
                    }
                    user.Password = HashProvider.ComputeHash(tempPassword, HashProvider.HashAlgorithmList.SHA256, _securityOptions.PasswordSalt);
                    _tmpPassword = tempPassword;
                    user.IsRemoved = false;
                    isWasRemoved = true;
                }
            }
            //Save user details
            if (user != null)
            {
                if (EmailAddress != user.EmailAddress || string.IsNullOrEmpty(EmailAddress))
                {
                    var Checkemail = _context.Users.Where(x => x.EmailAddress == EmailAddress).Any();
                    if (Checkemail)
                    {
                        errorMessage = _localizer[PublicEnums.LocalizationKeys.Profile_Email_Dup_Error];  // email exisits with other user 
                        return user.UserID;
                    }
                    else
                    {
                        user.EmailAddress = EmailAddress;
                    }
                }
                if(string.IsNullOrEmpty(IDNumber))
                {
                    errorMessage = _localizer[PublicEnums.LocalizationKeys.IDNumberValidationLength];
                    return user.UserID;
                }
                if (IDNumber != user.IDNumber)
                {
                    var country = _context.Countries.Where(x => x.CountryID == Guid.Parse(SelectedCountry)).FirstOrDefault();
                    if (country.IDNumberValidationLength != IDNumber.Count())
                    {
                        errorMessage = _localizer[PublicEnums.LocalizationKeys.IDNumberValidationLength];
                        return user.UserID;
                    }
                    else
                    {
                        user.IDNumber = IDNumber;
                        
                    }
                }

                // check that the dropdowns have values
                if (string.IsNullOrEmpty(SelectedProvince) || SelectedProvince == Guid.Empty.ToString())
                {
                    errorMessage = _localizer[PublicEnums.LocalizationKeys.Province_Please_Select];
                    UserID = user.UserID;
                    return UserID;
                }
                else
                {
                    user.ProvinceID = Guid.Parse(SelectedProvince);
                }
                if (string.IsNullOrEmpty(SelectedCountry) || SelectedCountry == Guid.Empty.ToString())
                {
                    errorMessage = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
                    UserID = user.UserID;

                    return UserID;
                }
                else
                {
                    user.CountryID = Guid.Parse(SelectedCountry);
                }

                if (SelectedLanguageCultureID != "" && SelectedLanguageCultureID != null)
                {
                    user.LanguageCultureID = Guid.Parse(SelectedLanguageCultureID);
                }
                if (string.IsNullOrEmpty(DisplayName)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Display_Name]; return UserID; } else { user.DisplayName = DisplayName; }
                if (string.IsNullOrEmpty(EmailAddress)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Email_Address]; return UserID; } else { user.EmailAddress = EmailAddress; }
                if (string.IsNullOrEmpty(FirstName)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_First_Name]; return UserID; } else { user.FirstName = FirstName; }
                if (string.IsNullOrEmpty(Surname)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Last_Name]; return UserID; } else { user.Surname = Surname; }
                if (string.IsNullOrEmpty(Title)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Name_Title]; return UserID; } else { user.Title = Title; }

                user.ReceiveEmailNotification = ReceiveEmailNotifactions;
                user.IsSuspended = IsSuspended;
                user.LoginTries = (IsSuspended == false) ? 0 : user.LoginTries;
                user.EditUserID = userHelper.loggedInUserID;
                user.EditDateTime = DateTime.UtcNow;
                user.Timezone = SelectedTimezone;
                user.IsEmailVerified = IsEmailVerified;
                user.ReceiveEmailNotification = ReceiveEmailNotifactions;
                user.IsAdminApproved = IsAdminApproved;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    AzureStorageHelperFunctions helper = new AzureStorageHelperFunctions();
                    helper._securityOptions = _securityOptions;
                    helper._fileStorageOptions = _fileStorageOptions;
                    helper._env = _env;

                    if (!string.IsNullOrEmpty(user.ProfileImageName))
                    {
                        await helper.DeleteBlob(user.ProfileImageName);
                    }

                    user.ProfileImageName = Guid.NewGuid().ToString();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        ImageFile.CopyTo(stream);
                        await helper.UploadBlob(stream.ToArray(), user.ProfileImageName);
                    }
                }

                if (isNew)
                {
                    _context.Add(user);
                    user.Audit(_context, PublicEnums.DataAuditAction.ACTION_ADD);
                }
                else
                {
                    _context.Update(user);
                    user.Audit(_context, PublicEnums.DataAuditAction.ACTION_UPDATE);
                }
                if (isNew || isWasRemoved)
                {
                    #region  Send new user registration email
                    if (!string.IsNullOrEmpty(EmailAddress))
                    {
                        emailTemplate = PublicEnums.EmailTemplateList.NTF_REGISTRATION_WELCOME_CUSTOM;
                        var variables = new Dictionary<string, PropertyMetaData>
                    {
                        {"HostUrl", new PropertyMetaData {Type = typeof (string), Value = _securityOptions.WebsiteHostUrl}},
                        {"DisplayName", new PropertyMetaData {Type = typeof (string), Value = DisplayName}},
                        {"Password", new PropertyMetaData {Type = typeof (string), Value = tempPassword}},
                        {"Username", new PropertyMetaData {Type = typeof (string), Value = EmailAddress}}
                    };
                        await _emailService.SendEmailAsync(new List<string>() { EmailAddress }, "Welcome", emailTemplate, variables, _user);
                    }
                    #endregion
                }
                await ClearUserRoles(user.UserID);
                await AddSelectedUserRoles(user.UserID);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string g = ex.ToString();
                }
                
                    
            }
            else
            {
                errorMessage = _localizer[PublicEnums.LocalizationKeys.Generic_Error_Message];
            }
            
            UserID = user.UserID;
            return UserID;
        }
        internal async Task<bool> UpdateUserProfile()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user,
            };
            userHelper.Populate();

            var user = _context.Users.FirstOrDefault(x => x.UserID == UserID);
            if (user != null)
            {
                if (EmailAddress != user.EmailAddress)
                {
                    var Checkemail = _context.Users.Where(x => x.EmailAddress == EmailAddress).Any();
                    if (Checkemail)
                    {
                        errorMessage = _localizer[PublicEnums.LocalizationKeys.Profile_Email_Dup_Error];  // email exisits with other user 
                        return false;
                    }
                }
                if (IDNumber != user.IDNumber)
                {
                    var country = _context.Countries.Where(x => x.CountryID == Guid.Parse(SelectedCountry)).FirstOrDefault();
                    if (country.IDNumberValidationLength != IDNumber.Count())
                    {
                        errorMessage = _localizer[PublicEnums.LocalizationKeys.IDNumberValidationLength];
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(DisplayName)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Display_Name]; return false; } else { user.DisplayName = DisplayName; }
                if (string.IsNullOrEmpty(EmailAddress)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Email_Address]; return false; } else { user.EmailAddress = EmailAddress; }
                
                user.EditUserID = userHelper.loggedInUserID;
                user.EditDateTime = DateTime.UtcNow;

                if (string.IsNullOrEmpty(FirstName)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_First_Name]; return false; } else { user.FirstName = FirstName; }
                if (string.IsNullOrEmpty(Surname)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Last_Name]; return false; } else { user.Surname = Surname; }

                user.Surname = Surname;
                user.Timezone = SelectedTimezone;

                if (string.IsNullOrEmpty(Title)) { errorMessage = _localizer[PublicEnums.LocalizationKeys.Validate_Name_Title]; return false; } else { user.Title = Title; }
               
                user.IDNumber = IDNumber;

                user.ReceiveEmailNotification = ReceiveEmailNotifactions;

                // check that the dropdowns have values
                if (string.IsNullOrEmpty(SelectedProvince) || SelectedProvince == Guid.Empty.ToString())
                {
                    errorMessage = _localizer[PublicEnums.LocalizationKeys.Province_Please_Select];
                    return false;
                }
                if (Guid.Parse(SelectedCountry) != user.CountryID) // if diffrent from stored then lookup else leave. 
                {
                    user.CountryID = Guid.Parse(SelectedCountry);
                }
                if (Guid.Parse(SelectedProvince) != user.ProvinceID)
                {
                    user.ProvinceID = Guid.Parse(SelectedProvince);
                }
                if (SelectedLanguageCultureID != "" && SelectedLanguageCultureID != null)
                {
                    user.LanguageCultureID = Guid.Parse(SelectedLanguageCultureID);
                }

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    AzureStorageHelperFunctions helper = new AzureStorageHelperFunctions();
                    helper._securityOptions = _securityOptions;
                    helper._fileStorageOptions = _fileStorageOptions;
                    helper._env = _env;

                    if (!string.IsNullOrEmpty(user.ProfileImageName))
                    {
                        await helper.DeleteBlob(user.ProfileImageName);
                    }

                    user.ProfileImageName = Guid.NewGuid().ToString();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        ImageFile.CopyTo(stream);
                        await helper.UploadBlob(stream.ToArray(), user.ProfileImageName);
                    }
                }

                _context.Update(user);
                user.Audit(_context, PublicEnums.DataAuditAction.ACTION_UPDATE);
                await _context.SaveChangesAsync();
                UserID = user.UserID;
                return true;
            }
            errorMessage = _localizer[PublicEnums.LocalizationKeys.User_Profile_Update_Fail];
            return false;
        }
        public class UserDetailsViewModelRoles
        {
            public Guid UserRoleID { get; set; }
            public string Description { get; set; }
            public string EventCode { get; set; }
            public bool Selected { get; set; }
        }
    }
}
