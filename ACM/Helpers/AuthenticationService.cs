using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ACM.Models.AccountDataModelFactory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace ACM.Helpers
{
    public class AuthenticationService
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal HttpContext _httpContext;
        internal IDistributedCache _cache;

        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }

        public ClaimsPrincipal SignedInIdentity { get; set; }

        public AuthenticationService()
        {

        }

        public async Task<AuthenticationResult> SignIn(String emailAddressUsername, String password, bool createSignInCookie = true)
        {
            int retryLimit = int.Parse(_context.SystemConfiguration.Where(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_LOGIN_RETRYLIMIT.ToString()).First().ConfigValue);

            string hashedPassword = HashProvider.ComputeHash(password, HashProvider.HashAlgorithmList.SHA256, _securityOptions.PasswordSalt);

            var user = _context.Users.FirstOrDefault(x => x.IsRemoved == false
            && (x.EmailAddress == emailAddressUsername && x.EmailAddress != null)
            && x.Password == hashedPassword);

            if (user != null)
            {
                var userRole = _context.LinkUserRole.Include(x => x.UserRole).FirstOrDefault(x => x.UserID == user.UserID);

                if (user.IsSuspended)
                {
                    return new AuthenticationResult("Your account is suspended. Please reset your password or ask your administrator to un-suspend your account.");
                }
                else if (user.IsEmailVerified == false)
                {
                    return new AuthenticationResult("Your email address have not been verified. Please click the link in the email that was sent to you.");
                }
                //else if ((userRole.UserRole.EventCode == PublicEnums.UserRoleList.ROLE_COACH) && user.IsAdminApproved == false)
                //{
                //    return new AuthenticationResult("Your account has not yet been approved by an Administrator. Please try again later once your account have been approved");
                //}
                else
                {
                    var identity = CreateIdentity(user, true);

                    if (user.LoginTries != 0 || user.IsSuspended == true)
                    {
                        user.LoginTries = 0;
                        user.IsSuspended = false;
                        user.EditDateTime = DateTime.UtcNow;
                        user.EditUserID = user.UserID;

                        _context.Update(user);

                        _context.SaveChanges();
                    }

                    if (createSignInCookie)
                    {
                        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
                        {
                            IsPersistent = true
                        });
                    }
                }
            }
            else
            {
                var userObj = _context.Users.FirstOrDefault(x => x.IsRemoved == false
                && (x.EmailAddress == emailAddressUsername && x.EmailAddress != null));

                //Check user onboarded by admin
                if (userObj == null)
                {
                    return new AuthenticationResult("Your email address have not yet been registered as a user on this system.");
                }
                else if (userObj.IsSuspended)
                {
                    return new AuthenticationResult("Your account is suspended. Please reset your password or ask your administrator to un-suspend your account.");
                }
                else if (userObj.IsEmailVerified == false)
                {
                    return new AuthenticationResult("Your email address have not been verified. Please click the link in the email that was sent to you.");
                }
                else
                {
                    if (userObj.LoginTries + 1 >= retryLimit)
                    {
                        userObj.LoginTries = 0;
                        userObj.IsSuspended = true;

                        userObj.EditUserID = userObj.UserID;
                        userObj.EditDateTime = DateTime.UtcNow;
                        _context.Update(userObj);

                        _context.SaveChanges();

                        return new AuthenticationResult("Email or Password is not correct. Your account have been suspended. Please reset your password or contact your administrator.");
                    }
                    else
                    {
                        userObj.LoginTries++;

                        userObj.EditUserID = userObj.UserID;
                        userObj.EditDateTime = DateTime.UtcNow;
                        _context.Update(userObj);

                        _context.SaveChanges();

                        return new AuthenticationResult($"Email or Password is not correct. Login tries {userObj.LoginTries} / {retryLimit}");
                    }
                }
            }

            return new AuthenticationResult();
        }

        public async Task<AuthenticationResult> SignIn(User user)
        {
            var identity = CreateIdentity(user, true);

            if (user.LoginTries != 0 || user.IsSuspended == true)
            {
                user.LoginTries = 0;
                user.IsSuspended = false;
                user.EditDateTime = DateTime.UtcNow;
                user.EditUserID = user.UserID;

                _context.Update(user);

                _context.SaveChanges();
            }

            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
            {
                IsPersistent = true
            });

            return new AuthenticationResult();
        }

        internal ClaimsPrincipal CreateIdentity(User user, bool setCache)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            var identity = new ClaimsIdentity("ACMAuthenticationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID", user.UserID.ToString()));

            //Get user roles
            var roles = _context.LinkUserRole.Include(x => x.UserRole).Where(x => x.UserID == user.UserID).ToList();
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.UserRole.EventCode));
            }

            identity.AddClaim(new Claim(ClaimTypes.Name, user.DisplayName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.Surname ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.EmailAddress));
            identity.AddClaim(new Claim("Timezone", user.Timezone ?? ""));

            if(user.LanguageCultureID != null)
            {
                var language = _context.LanguageCultures.FirstOrDefault(x => x.LanguageCultureID == user.LanguageCultureID);
                if(language != null)
                {
                    identity.AddClaim(new Claim("CultureNameCode", language.CultureNameCode));
                    identity.AddClaim(new Claim("LanguageCultureID", language.LanguageCultureID.ToString()));

                    if (_cache != null && setCache == true)
                    {
                        string jsonResponse = JsonConvert.SerializeObject(_context.LocalizationValues.AsNoTracking().Where(x => x.LanguageCultureID == user.LanguageCultureID).ToList());
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
                        var options = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromDays(30));

                        _cache.Set("Localization_" + language.CultureNameCode.ToLower(), jsonBytes, options);
                    }
                }
            }

            if (!String.IsNullOrEmpty(user.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));
            }

            // add your own claims if you need to add more information stored on the cookie
            //Look up roles
            principal.AddIdentity(identity);

            SignedInIdentity = principal;
            return principal;
        }
    }
}
