using Logger.AzureApplicationInsight.Analytics.ClientSide.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Logger.AzureApplicationInsight.Analytics.ClientSide.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //_telemetryClient.TrackPageView("Home/Index Page..!");
            return View();
        }

        public IActionResult Privacy()
        {
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
            //_telemetryClient.TrackEvent("Form Submitted.");

            return RedirectToAction("Index");
        }
    }
}
