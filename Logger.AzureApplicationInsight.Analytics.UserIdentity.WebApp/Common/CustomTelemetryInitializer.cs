using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
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
            var requestTelemetry = telemetry as RequestTelemetry;            
            if (requestTelemetry == null)// Is this a TrackRequest() ?
                return;

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

        private void AddTelemetry_ContextPropertyFromClaims(ITelemetry telemetry, string claimName)
        {

            var requestTelemetry = telemetry as RequestTelemetry;
            
            if (requestTelemetry == null)// Is this a TrackRequest() ?
                return;

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                var claim = httpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(claimName, StringComparison.InvariantCultureIgnoreCase));

                if (claim != null)
                {
                    requestTelemetry.Properties[claimName] = claim.Value;
                }
            }
        }

        private void AddTelemetry_CustomProperty(ITelemetry telemetry)//, string propName, string propValue)
        {
            var requestTelemetry = telemetry as RequestTelemetry;

            if (requestTelemetry == null)// Is this a TrackRequest() ?
                return;

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                var propVal = (string)httpContext.Items["MyCustomProp"];

                if (propVal != null)
                {
                    requestTelemetry.Properties["MyCustomProp"] = propVal;
                }
            }
        }

    }
}
