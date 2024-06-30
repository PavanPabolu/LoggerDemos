using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;

namespace Logger.AzureApplicationInsight.Analytics.ServerSide.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public BaseController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        protected void TrackPageViewPerformance(string pageName)
        {
            // Track page view with performance data
            var pageViewTelemetry = new PageViewTelemetry(pageName)
            {
                Duration = TimeSpan.FromSeconds(1.5) // Simulate page load time
            };
            _telemetryClient.TrackMetric(pageName, Convert.ToDouble(pageViewTelemetry.Duration));
        }

        protected string GetDeviceType(HttpRequestMessage request)
        {
            var userAgent = request.Headers.GetValues("User-Agent").FirstOrDefault();
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            //var deviceDetector = new DeviceDetector(userAgent);
            //if (deviceDetector.IsMobile)
            //    return "Mobile";
            //else if (deviceDetector.IsTablet)
            //    return "Tablet";
            //else
            //    return "Desktop";

            // Check if the user agent string contains "Mobile" or "Android"
            if (userAgent.Contains("Mobile") || userAgent.Contains("Android"))
                return "Mobile";
            else if (userAgent.Contains("Tablet"))
                return "Tablet";
            else
                return "Desktop";
        }

    }
}
