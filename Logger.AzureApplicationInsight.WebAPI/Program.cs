using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();
builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights("0c4e73f1-8e95-4723-87a4-c863e6b2e742");



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//-----------------------------------------------

builder.Services.AddApplicationInsightsTelemetry();


//-----------------------------------------------


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




//-----------------------------------------------

app.Logger.LogTrace("Adding Routes");
app.MapGet("/", () => "Hello World!");
app.MapGet("/Test", async (ILogger<Program> logger, HttpResponse response) =>
{
    logger.LogInformation("Testing logging in Program.cs");
    await response.WriteAsync("Testing");
});
app.Logger.LogInformation("Starting the app");

//-----------------------------------------------



app.Run();
