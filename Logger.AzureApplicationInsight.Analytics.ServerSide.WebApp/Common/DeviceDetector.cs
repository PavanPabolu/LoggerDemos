namespace Logger.AzureApplicationInsight.Analytics.ServerSide.WebApp.Common
{
    public static class DeviceDetector
    {
        public static string GetDeviceType(HttpRequest request)//HttpRequestMessage request)
        {
            //var userAgent = request.Headers.GetValues("User-Agent").FirstOrDefault();
            var userAgent = request.Headers["User-Agent"].FirstOrDefault();
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
