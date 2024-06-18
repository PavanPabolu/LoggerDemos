using Logger.Serilog.AzureApplicationInsight.WebAPI;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//--------------------------------
/*
//var configuration = builder.Configuration;
// Read configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
Serilog.Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration).CreateLogger();
*/

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
