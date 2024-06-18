1.
Create a New ASP.NET Core 8 Project:
Open Visual Studio or your preferred IDE.
Create a new ASP.NET Core Web Application.
Choose ASP.NET Core 8 and select the template you prefer (e.g., Web API).

2.
Install Required NuGet Packages:
Open the NuGet Package Manager.
Install the following packages:
Serilog.AspNetCore
Serilog.Sinks.ApplicationInsights
Serilog.Settings.Configuration

You can also install these packages using the .NET CLI:
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.ApplicationInsights
dotnet add package Serilog.Settings.Configuration

3. 
Add Application Insights Instrumentation Key:
In your appsettings.json file, add your Application Insights instrumentation key:
{
  "ApplicationInsights": {
    "InstrumentationKey": "YOUR_INSTRUMENTATION_KEY"
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.ApplicationInsights" ],
    "WriteTo": [
      {
        "Name": "ApplicationInsights",
        "Args": {
          "instrumentationKey": "YOUR_INSTRUMENTATION_KEY",
          "telemetryConverter": "Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters.TraceTelemetryConverter, Serilog.Sinks.ApplicationInsights"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
    "Properties": {
      "Application": "SampleApp"
    }
  }
}

4.
Configure Serilog in Program.cs:
Open the Program.cs file and configure Serilog during the application startup:
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;

public class Program
{
    public static void Main(string[] args)
    {
        // Read configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {
            Log.Information("Starting up the application");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog() // Add this line to use Serilog
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

5. 
Verify Logging in Application Insights:
Run your application.
Generate some log entries by accessing various endpoints or performing actions within your application.
Go to the Azure Portal and navigate to your Application Insights resource.
Check the Logs section to see if your Serilog entries are appearing.







































