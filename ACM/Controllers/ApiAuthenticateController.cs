using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace ACM.Controllers
{
    [Produces("application/json")]
    [Route("api/authenticate")]
    [ApiController]
    [EnableCors]
    public class ApiAuthenticateController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        public ApiAuthenticateController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Authenticates a user using Email and Password and returns a JWT token with claims
        /// </summary>
        /// <param name="applicationUser">User credentials</param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors]
        [AllowAnonymous]
        [Produces(typeof(JwtToken))]
        public async Task<IActionResult> Post([FromBody] AuthenticateRequest applicationUser)
        {
            try
            {
                var jwtOptions = new JwtIssuerOptions()
                {
                    Issuer = _jwtOptions.Issuer,
                    Audience = _jwtOptions.Audience,
                    SigningCredentials = _jwtOptions.SigningCredentials
                };
                jwtOptions.ValidFor = TimeSpan.FromDays(double.Parse(_context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_LOGIN_TOKEN_VALID_MIN.ToString()).ConfigValue));

                JwtTokenValidator validator = new JwtTokenValidator();
                validator._context = _context;
                validator._jwtOptions = jwtOptions;
                validator._securityOptions = _securityOptions;

                if (TryValidateModel(applicationUser))
                {
                    if (await validator.ValidateAuthentication(applicationUser, HttpContext))
                    {
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(validator.Token);

                        // Serialize and return the response
                        var response = new JwtToken()
                        {
                            access_token = encodedJwt,
                            expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
                            expires_on = jwtOptions.Expiration,
                        };

                        return new OkObjectResult(response);
                    }
                    else
                    {
                        return BadRequest("Invalid credentials supplied. Unable to authenticate user");
                    }
                }
                else
                {
                    return BadRequest("Not all required fields have been supplied");
                }
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiAuthenticateController.Post", ex.Message, User, ex);
                return BadRequest("An error occurred while processing your request");
            }
        }
    }
}
