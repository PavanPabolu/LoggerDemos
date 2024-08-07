using Logger.AzureApplicationInsight.Analytics.ServerSide.WebApp.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging
using System.Diagnostics;

namespace Logger.AzureApplicationInsight.Analytics.ServerSide.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryClient _telemetryClient;

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            //_telemetryClient.TrackPageView("Home/Index Page..!");
            return View();
        }

        public IActionResult Privacy()
        {
            //_telemetryClient.TrackPageView("Home/Privacy Page..!");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SubmitForm()
        {
            // Track button click event
            _telemetryClient.TrackEvent("Form Submitted.");
            _telemetryClient.TrackEvent("Form Submit Button Clicked", new Dictionary<string, string>
            {
                { "ButtonName", "btn-1" }
            });

            return RedirectToAction("Index");
        }




        [Route("random")]
        [HttpGet]
        [HttpOptions]
        public async Task<IActionResult> Delay()
        {
            var random = new Random();
            var num = random.Next(10, 10000);

            await Task.Delay(num);

            if (num > 9990) throw new Exception("Random number is greater than 9990");

            return Ok(num);
        }
    }
}
