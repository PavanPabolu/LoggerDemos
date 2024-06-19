// See https://aka.ms/new-console-template for more information
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


Console.WriteLine("Hello, World!");

const string aiConnectionString = "InstrumentationKey=04caef53-d39a-4b91-bf4c-1c9b1182ebef;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/;ApplicationId=9464baee-0c4c-466e-95bd-2f52a9e1192f";

using var channel = new InMemoryChannel(); //Microsoft.ApplicationInsights.Channel

try
{
    /*
    IServiceCollection services = new ServiceCollection();
    services.Configure<TelemetryConfiguration>(config => config.TelemetryChannel = channel);
    services.AddLogging(builder =>
    {
        // Only Application Insights is registered as a logger provider
        builder.AddApplicationInsights(
            configureTelemetryConfiguration:(config) => config.ConnectionString = aiConnectionString,
            configureApplicationInsightsLoggerOptions: (options) => { }
            );
        builder.AddConsole(); //dotnet add package Microsoft.Extensions.Logging.Console
    });
    services.AddScoped<Sample>();

    //Configure the logging provider - After the service collection ready, build to get the service provider
    IServiceProvider serviceProvider = services.BuildServiceProvider();
    */

    //OR

    // Configure the logging provider
    var serviceProvider = new ServiceCollection()
        .Configure<TelemetryConfiguration>(config => config.TelemetryChannel = channel)        
        .AddLogging(builder =>
        {
            //register log providers
            builder.AddApplicationInsights(
                configureTelemetryConfiguration: (config) => config.ConnectionString = aiConnectionString,
                configureApplicationInsightsLoggerOptions: (options) => { } //options.TrackExceptionsAsExceptionTelemetry = false;
            );
            builder.AddConsole(); //dotnet add package Microsoft.Extensions.Logging.Console
        })
        .AddScoped<Sample>()
        .BuildServiceProvider();


    ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogWarning($"Logger from the ConsoleApp-Program.[{DateTime.Now.ToShortTimeString()}]");

    //concrete class method call
    var samp = serviceProvider.GetRequiredService<Sample>();
    samp.PerformOperation();

    //helper class method call
    LoggingHelper.LogSomeMessages(logger);

    //for ExceptionTelemetry
    var x = 0;
    var y = 10 / x;
}
finally
{
    // Explicitly call Flush() followed by Delay, as required in console apps.
    // This ensures that even if the application terminates, telemetry is sent to the back end.
    channel.Flush();

    await Task.Delay(TimeSpan.FromMilliseconds(1000));
}

//Concrete class
public class Sample
{
    private readonly ILogger<Sample> _logger;

    public Sample(ILogger<Sample> logger)
    {
        _logger = logger;
    }

    public void PerformOperation()
    {
        _logger.LogInformation("Sample class - PerformOperation method called.");
        _logger.LogWarning("Sample class - PerformOperation method called.");
    }
}

//Helper class
public static class LoggingHelper
{
    public static void LogSomeMessages(ILogger logger)
    {
        logger.LogDebug("This is a debug message.");
        logger.LogInformation("This is an informational message.");
        logger.LogWarning("This is a warning message.");
        logger.LogError("This is an error message.");
        logger.LogCritical("This is a critical message.......");
    }
}

