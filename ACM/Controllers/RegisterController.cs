using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ACM.Models;
using ACM.Models.AccountDataModelFactory;
//using ACM.Models.VoucherModelFactory;
using ACM.Models.UserModelFactory;
using ACM.Services;
using ACM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ACM.Helpers;

namespace ACM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class RegisterController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ILogger<RegisterController> _logger;
        //private readonly ACM _SingoProServices;
        //private readonly SingoNetworkAPI _SingoPro;
        private readonly SecurityOptions _securityOptions;

        public RegisterController(IOptions<SecurityOptions> securityOptions, AppDBContext context, ILogger<RegisterController> loggersingoservice)
        {
            _securityOptions = securityOptions.Value;
            _context = context;
        }
        [HttpPost]
        [EnableCors]
        public async Task<IActionResult> Register(RegisterViewModel u)
        {
            //var password = "password";
            //string hashedPassword = HashProvider.ComputeHash(password, HashProvider.HashAlgorithmList.SHA256, securityOptions.PasswordSalt);
            User s = new User();
            s.UserID = Guid.NewGuid();
            s.CreatedUserID = s.UserID;
            s.EditUserID = s.UserID;
            s.Password = HashProvider.ComputeHash(u.Password, HashProvider.HashAlgorithmList.SHA256, _securityOptions.PasswordSalt);
            s.DisplayName = u.DisplayName;
            s.EmailAddress = u.EmailAddress;
            s.CellphoneNumber = u.CellphoneNumber;
            s.LoginTries = 0;
            s.IsSuspended = false;
            s.IsRemoved = false;
            s.AcceptTermsAndConditions = true;
            s.IsEmailVerified = true;
            s.FirstName = u.FirstName;
            s.Surname = u.Surname;
            s.Title = u.Title;
            s.IDNumber = u.IDNumber;
            s.ReceiveEmailNotification = false;
            //s.OrganisationID = or.OrganisationID;
            s.LanguageCultureID = _context.LanguageCultures.First(x => x.CultureNameCode == "en-ZA").LanguageCultureID;
            s.CountryID = _context.Countries.First(x => x.Description == "South Africa").CountryID;
            s.ProvinceID = _context.Provinces.First(x => x.ProvIsoCode == "za-GP").ProvinceID;
            s.Timezone = "South Africa Standard Time";
            s.CreatedDateTime = DateTime.Now;
            s.EditDateTime = DateTime.Now;
            //s.AcmAccessRoleID = Guid.Parse("728D83BB-A3A9-4E3D-A778-B8EDEF5D506B");


            //s.ReceiveSMSNotification = u.ReceiveSMSNotification;
            //s.ReceiveAllCaseChangeNotification = u.ReceiveAllCaseChangeNotification;
            //s.ReceiveImportantChangeNotification = u.ReceiveImportantChangeNotification;
            //s.ManagerID = u.ManagerID;
            //s.DepartmentID = u.DepartmentID;
            //s.FirstLogIn = u.FirstLogIn;
            try
            {
                _context.Add(s);
                await _context.SaveChangesAsync();
                return Ok("User Successfully Registered");
            }
            catch (Exception x)
            {
                return Ok(x);
            }
        }
    }
}
