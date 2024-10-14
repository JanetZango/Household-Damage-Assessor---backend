using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using ACM.Models.AccountDataModelFactory;
using ACM.Models.UserModelFactory;
using ACM.ViewModels;
using ACM.ViewModels.CalendarViewModelFactory;
using ACM.ViewModels.HouseViewModelFactory;
using ACM.ViewModels.UsersViewModelFactory;
using App.TemplateParser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MomentSharp.Globalization;
using School.Models.HouseModelFactory;
using System.IdentityModel.Tokens.Jwt;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ACM.Controllers
{
    [Produces("application/json")]
    [Route("api/house")]
    [ApiController]
    [EnableCors]
    public class ApiHouseController : ControllerBase
	{
		private readonly AppDBContext _context;
		private readonly SecurityOptions _securityOptions;
		private readonly IEmailService _emailService;
		private readonly JwtIssuerOptions _jwtOptions;
		private readonly IConfiguration _configuration;
		private readonly IDistributedCache _cache;
		private readonly IStringLocalizer<SessionStringLocalizer> _localizer;

		public ApiHouseController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService,
			IOptions<JwtIssuerOptions> jwtOptions, IConfiguration configuration, IDistributedCache cache, IStringLocalizer<SessionStringLocalizer> localizer)
		{
			_context = context;
			_securityOptions = securityOptions.Value;
			_emailService = emailService;
			_jwtOptions = jwtOptions.Value;
			_configuration = configuration;
			_cache = cache;
			_localizer = localizer;
		}

		[HttpGet]
		[Route("GetHouses")]
		[EnableCors]
		public async Task<IActionResult> GetHouses()
		{
			try
			{
                HousesListViewModel model = new HousesListViewModel();
                model._context = _context;
				model._emailService = _emailService;
				model._securityOptions = _securityOptions;
				model._user = User;


				await model.PopulateLists();

				return new OkObjectResult(model);
			}
			catch (Exception ex)
			{
				HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiCasesController.GetHouses", ex.Message, User, ex);
				return BadRequest(_localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value);
			}
		}


        [HttpGet]
        [Route("GetHouseDetails")]
        [EnableCors]
        public async Task<IActionResult> GetHouseDetails(string ID)
        {
            try
            {

                HousesViewModel model = new HousesViewModel();
                model._context = _context;
                model._emailService = _emailService;
                model._securityOptions = _securityOptions;
                model._user = User;

                model.HouseID = Guid.Parse(ID);
                await model.PopulateDetails();

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiCasesController.GetHouseDetails", ex.Message, User, ex);
                return BadRequest(_localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value);
            }
        }
        [AllowAnonymous]
		[HttpPost]
		[Route("SaveHouse")]
		public async Task<IActionResult> SaveHouse(HousesViewModel model)
		{
			try
			{
				model._context = _context;
				model._user = User;

                await model.Save();

				return Ok("Successfully registered");

			}
			catch (Exception ex)
			{
				HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.ApiAccountController.Profile", ex.Message, User, ex);
				return BadRequest(_localizer[PublicEnums.LocalizationKeys.User_Profile_Update_Fail].Value);
			}
		}


		[HttpGet]
		[Route("DeleteHouse")]
		[EnableCors]
		public async Task<IActionResult> DeleteHouse(string ID)
		{
			try
			{
				HousesViewModel model = new HousesViewModel();
				model._context = _context;
				model._user = User;
				model.HouseID = Guid.Parse(ID);

				bool removed = await model.Remove();
				if (removed)
				{
					return Ok(new { Result = true, Message = _localizer[PublicEnums.LocalizationKeys.Removal_Successfull].Value });
				}
				else
				{
					return Ok(new { Result = false, Message = _localizer[PublicEnums.LocalizationKeys.Confirm_Removal].Value });
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.Log(_context, PublicEnums.LogLevel.LEVEL_EXCEPTION, "Controllers.UsersController.RemoveUser", ex.Message, User, ex);
				return Ok(_localizer[PublicEnums.LocalizationKeys.Generic_Error_Message].Value);
			}
		}
	}
}
