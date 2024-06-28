using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//-----------------------------------------------

builder.Services.AddApplicationInsightsTelemetry();

//-----------------------------------------------

//You can call the Application Insights trace API directly. The logging adapters use this API.
//https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-trace-logs#:~:text=Use%20the%20Trace%20API%20directly
var telemetryConfig = TelemetryConfiguration.CreateDefault();
var telemetryClient = new TelemetryClient(telemetryConfig);
telemetryClient.TrackTrace("Program/Main method called!");

//-----------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//-----------------------------------------------

app.Logger.LogInformation("Program/Main - Startup");
app.Logger.LogTrace("Program/Main - Startup");

app.MapGet("/Test", async (ILogger<Program> Logger, HttpResponse response) =>
{
    Logger.LogInformation("Program/Test called");

    await response.WriteAsync("Program/Test Response");
});

//-----------------------------------------------

app.Run();
