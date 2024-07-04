using Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Common;
using Microsoft.ApplicationInsights.Extensibility;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddHttpContextAccessor();
// Add Telemetry Initializer
builder.Services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitializer>();
// Add Application Insights
builder.Services.AddApplicationInsightsTelemetry();




builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
