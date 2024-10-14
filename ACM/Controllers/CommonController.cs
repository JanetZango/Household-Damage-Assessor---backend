using ACM.Helpers.EmailServiceFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Wangkanai.Detection.Services;
using Microsoft.Extensions.Localization;
using ACM.Helpers.Localization;
using Microsoft.AspNetCore.Hosting;

namespace ACM.Controllers
{
    public class CommonController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _pdfcache;
        private readonly IDetectionService _detectionService;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IStringLocalizer<SessionStringLocalizer> _localizer;
        private readonly IWebHostEnvironment _env;
        private readonly FileStorageOptions _fileStorageOptions;

        public CommonController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IEmailService emailService, IMemoryCache pdfcache,
            IDetectionService detectionService, IOptions<JwtIssuerOptions> jwtOptions,
            IStringLocalizer<SessionStringLocalizer> localizer,IWebHostEnvironment env,IOptions<FileStorageOptions> fileStorageOptions)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _env = env;
            _fileStorageOptions = fileStorageOptions.Value;
            _emailService = emailService;
            _pdfcache = pdfcache;
            _detectionService = detectionService;
            _jwtOptions = jwtOptions.Value;
            _localizer = localizer;
        }


        [HttpGet]
       

        public async Task<JsonResult> GetCountryProvinces(string selected)
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


            if (selected != "")
            {
                model.OptionsList = (from c in await userHelper.GetRealatedProvinces(selected)
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

            return Json(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RenderImageGen(string ID, int width = 0, int height = 0)
        {
            bool emptyFile = false;
            var content = new byte[0];
            string etag = "";

            if (!string.IsNullOrEmpty(ID))
            {
                AzureStorageHelperFunctions helper = new AzureStorageHelperFunctions();
                helper._securityOptions = _securityOptions;
                helper._env = _env;
                helper._fileStorageOptions = _fileStorageOptions;
                byte[] ImageBlob = await helper.DownloadBlob(ID);

                if (width > 0 && height > 0 && ImageBlob != null)
                {
                    ImageBlob = HelperFunctions.ResizeImagePreportional(ImageBlob, width, height, 100).ToArray();
                }

                content = ImageBlob;
                if (helper.downloadBlobProperties != null)
                {
                    etag = helper.downloadBlobProperties.ETag.ToString();
                }
            }
            else
            {
                emptyFile = true;
            }

            if (!emptyFile)
            {
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = ID.ToString() + ".png",
                    Inline = true,
                };

                Response.Headers["Cache-Control"] = $"public,max-age=14400";
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("ETag", etag);

                return File(content, "image/png");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
