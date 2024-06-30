using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

Console.WriteLine("Hello, World!");

/*
 install-package Microsoft.Extensions.Configuration.Abstractions
 install-package Serilog
 install-package Serilog.Sinks.Console
 install-package Serilog.Sinks.File
 install-package Serilog.Sinks.ApplicationInsights
 */


using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("file.txt")
    .WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = "INSIGHT_KEY" }, TelemetryConverter.Traces)
    .CreateLogger();

int i = 0;

while (i < 20)//(true)
{
    log.Information($"Hello from Serilog! {++i}");
    Thread.Sleep(1000);
}
