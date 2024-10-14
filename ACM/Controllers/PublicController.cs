using ACM.Services.ClickatellServiceFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ACM.Controllers
{
    [AllowAnonymous]
    public class PublicController : Controller
    {
        private readonly AppDBContext _context;
        private readonly SecurityOptions _securityOptions;
        private readonly IClickatellService _clickatellService;

        public PublicController(AppDBContext context, IOptions<SecurityOptions> securityOptions, IClickatellService clickatellService)
        {
            _context = context;
            _securityOptions = securityOptions.Value;
            _clickatellService = clickatellService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
