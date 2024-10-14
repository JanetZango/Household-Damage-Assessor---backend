using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using Wangkanai.Detection.Services;
using Microsoft.AspNetCore.Cors;

namespace ACM.Controllers
{
    [Produces("application/json")]
    [Route("api/common")]
    //[ApiController]
    public class ApiCommonController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _pdfcache;
        private readonly IDetectionService _detectionService;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;

        public ApiCommonController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService, IMemoryCache pdfcache,
            IDetectionService detectionService, IOptions<JwtIssuerOptions> jwtOptions,
            IStringLocalizer<SessionStringLocalizer> localizer)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _emailService = emailService;
            _pdfcache = pdfcache;
            _detectionService = detectionService;
            _jwtOptions = jwtOptions.Value;
            _localizer = localizer;
        }

        /// <summary>
        /// Get a list of countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors]
        [AllowAnonymous]
        [Produces(typeof(CascadingSelectListModel))]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            CascadingSelectListModel model = new CascadingSelectListModel();
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = User
            };
            userHelper.Populate();

            model.OptionsList = (from c in await userHelper.GetCountries()
                                 select new CascadingViewModel
                                 {
                                     label = c.Description,
                                     value = c.CountryID.ToString()
                                 }).ToList();

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Get a list of countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors]
        [AllowAnonymous]
        [Produces(typeof(CascadingSelectListModel))]
        [Route("GetGender")]
        public async Task<IActionResult> GetGender()
        {
            CascadingSelectListModel model = new CascadingSelectListModel();
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = User
            };
            userHelper.Populate();

            model.OptionsList = (from c in _context.Genders
                                 select new CascadingViewModel
                                 {
                                     label = c.Description,
                                     value = c.GenderID.ToString()
                                 }).ToList();

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Get a list of countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors]
        [AllowAnonymous]
        [Produces(typeof(CascadingSelectListModel))]
        [Route("GetEthnicity")]
        public async Task<IActionResult> GetEthnicity()
        {
            CascadingSelectListModel model = new CascadingSelectListModel();
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = User
            };
            userHelper.Populate();

            model.OptionsList = (from c in _context.Ethnicities
                                 select new CascadingViewModel
                                 {
                                     label = c.Description,
                                     value = c.EthnicityID.ToString()
                                 }).ToList();

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Get a list of provinces for a given country
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors]
        [AllowAnonymous]
        [Produces(typeof(CascadingSelectListModel))]
        [Route("GetCountryProvinces")]
        public async Task<IActionResult> GetCountryProvinces(string countryID)
        {
            CascadingSelectListModel model = new CascadingSelectListModel();
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = User
            };
            userHelper.Populate();


            if (countryID != "")
            {
                model.OptionsList = (from c in await userHelper.GetRealatedProvinces(countryID)
                                     select new CascadingViewModel
                                     {
                                         label = c.Description,
                                         value = c.ProvinceID.ToString()
                                     }).ToList();
            }
            else
            {
                model.OptionsList = new List<CascadingViewModel>();
            }

            return new OkObjectResult(model);
        }
    }
}
