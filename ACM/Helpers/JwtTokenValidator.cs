using Microsoft.Extensions.Caching.Distributed;
using System.IdentityModel.Tokens.Jwt;

namespace ACM.Helpers
{
    public class JwtTokenValidator
    {
        internal JwtIssuerOptions _jwtOptions;
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IDistributedCache _cache;

        public JwtSecurityToken Token { get; set; }
        public bool RequirePasswordSetOnLogin { get; set; }

        internal async Task<bool> ValidateAuthentication(AuthenticateRequest applicationUser, Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            bool returnValue = false;

            var authService = new AuthenticationService();
            authService._context = _context;
            authService._httpContext = httpContext;
            authService._securityOptions = _securityOptions;
            authService._cache = _cache;

            var authenticationResult = await authService.SignIn(applicationUser.EmailAddressUsername, applicationUser.Password, false);

            if (authenticationResult.IsSuccess)
            {
                // Create the JWT security token and encode it.
                Token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: authService.SignedInIdentity.Claims,
                    notBefore: _jwtOptions.NotBefore,
                    expires: _jwtOptions.Expiration,
                    signingCredentials: _jwtOptions.SigningCredentials);

                returnValue = true;
            }

            return returnValue;
        }
    }
}
