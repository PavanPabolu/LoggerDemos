
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Diagnostics;

namespace Logger.AzureApplicationInsight.Analytics.ServerSide.WebApp.Common.Middlewares
{
    public class ApplicationInsightsMiddleware : IMiddleware
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightsMiddleware(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        //public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{
        //    //Track PAge view
        //    _telemetryClient.TrackPageView($"Page view: {context.Request.Path}");

        //    //Call the next middleware in the pipeline
        //    await next(context);

        //    //Track Page Load time
        //    var pageLoadTime = context.Items["PageLoadTime"] as double?;
        //    if (pageLoadTime.HasValue)
        //        _telemetryClient.TrackMetric("Page Load Time", pageLoadTime.Value);

        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                //Track PAge view
                //_telemetryClient.TrackPageView($"Page view [Middleware]: {context.Request.Path}");

                // Detect device type
                var deviceType = DeviceDetector.GetDeviceType(context.Request);
                // Track page view with device type
                var pageViewTelemetry = new PageViewTelemetry($"Page view [Middleware]: {context.Request.Path}");
                pageViewTelemetry.Properties["DeviceType"] = deviceType;//Under CustomDimensions
                _telemetryClient.TrackPageView(pageViewTelemetry);


                //Call the next middleware in the pipeline
                var stopwatch = Stopwatch.StartNew();
                await next(context);
                stopwatch.Stop();

                //Track Page Load time
                context.Items["PageLoadTime"] = stopwatch.ElapsedMilliseconds;

                var pageLoadTime = context.Items["PageLoadTime"] as double?;
                if (pageLoadTime.HasValue)
                    _telemetryClient.TrackMetric("Page Load Time [Middleware]", pageLoadTime.Value);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
            }
        }
    }
}
