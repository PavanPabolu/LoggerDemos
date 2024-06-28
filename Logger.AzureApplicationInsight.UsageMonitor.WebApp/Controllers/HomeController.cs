using Logger.AzureApplicationInsight.UsageMonitor.WebApp.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging
using System.Diagnostics;

namespace Logger.AzureApplicationInsight.UsageMonitor.WebApp.Controllers
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
            _telemetryClient.TrackEvent("Form Submitted.");

            return RedirectToAction("Index");
        }
    }
}
