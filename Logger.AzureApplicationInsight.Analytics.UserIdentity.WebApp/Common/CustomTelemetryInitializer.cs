using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System.Security.Claims;

namespace Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Common
{
    /// <summary>
    /// This custom telemetry initializer to include the authenticated user's ID in the telemetry data.
    /// </summary>
    public class CustomTelemetryInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    telemetry.Context.User.Id = userId;
                }
            }
        }
    }
}
