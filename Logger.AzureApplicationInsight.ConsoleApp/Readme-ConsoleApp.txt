
Console application
https://learn.microsoft.com/en-us/azure/azure-monitor/app/ilogger?tabs=dotnet6#:~:text=Application%20Insights%3F.-,Console%20application,-To%20add%20Application
To add Application Insights logging to console applications, first install the following NuGet packages:
- Microsoft.Extensions.Logging.ApplicationInsights
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Logging.Console <-- add explicitly for console logs.


The following example uses the Microsoft.Extensions.Logging.ApplicationInsights package and demonstrates 
the default behavior for a console application. The Microsoft.Extensions.Logging.ApplicationInsights package 
should be used in a console application or whenever you want a bare minimum implementation of Application Insights 
without the full feature set such as metrics, distributed tracing, sampling, and telemetry initializers.
-------------------------------------------------------------------------------------------
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    services.Configure<TelemetryConfiguration>(config => config.TelemetryChannel = channel);
    services.AddLogging(builder =>
    {
        // Only Application Insights is registered as a logger provider
        builder.AddApplicationInsights(
            configureTelemetryConfiguration: (config) => config.ConnectionString = "<YourConnectionString>",
            configureApplicationInsightsLoggerOptions: (options) => { }
        );
    });

    IServiceProvider serviceProvider = services.BuildServiceProvider();
    ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Logger is working...");
}
finally
{
    // Explicitly call Flush() followed by Delay, as required in console apps.
    // This ensures that even if the application terminates, telemetry is sent to the back end.
    channel.Flush();

    await Task.Delay(TimeSpan.FromMilliseconds(1000));
}




