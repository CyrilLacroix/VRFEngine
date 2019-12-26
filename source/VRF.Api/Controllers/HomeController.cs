using VRFEngine.Common.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;

namespace VRFEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private Settings _settings;

        public HomeController(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public IActionResult Index()
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot", "index.html");

            return PhysicalFile(file, "text/html");
        }

        [Route("GetVersionInfo")]
        [HttpGet]
        public string GetVersionInfo()
        {
            return $"{_settings.Environment} - {typeof(Startup).Assembly.GetName().Version.ToString()}";
        }

        [Route("GetFrontendLogLevel")]
        [HttpGet]
        public string GetFrontendLogLevel()
        {
            return _settings.FrontendLogLevel;
        }
    }
}
