//using Microsoft.AspNetCore.Http.HttpResults;
//using Serilog.Events;
using Serilog;
using Logger.Serilog.ConsoleAndFile.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//--------------------------------
/*
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("Hello, world!");

//OR

// dotnet add package Serilog.Settings.Configuration - To use the console sink with Microsoft.Extensions.Configuration
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();

//var logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(configuration)
//    .CreateLogger();

//OR
// "dotnet add package serilog.sinks.async" - Console logging is synchronous and this can cause bottlenecks in some deployment scenarios. For high-volume console logging, consider using Serilog.Sinks.Async to move console writes to a background thread
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Async(wt => wt.Console())
//    .CreateLogger();
*/
//OR 

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());
//--------------------------------

builder.Services.AddControllers();
builder.Services.AddScoped<HelperService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// Minimal API endpoint
app.MapGet("/minimalapi", (ILogger<Program> logger) =>
{
    logger.LogInformation($"{logger.GetType()} - This is a log from minimal API endpoint.");
    Log.Information($"{Log.Logger.GetType()} - This is a log from minimal API endpoint.");

    return Results.Ok("Hello from minimal API");
});


try
{
    Log.Information("Starting up the application");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
