using Logger.HTTPlogging.WebAPI.Common;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//---------------------------------------------------

//Add HTTP Logging Middleware - specifying which parts of the HTTP request and response to log.
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All; //Microsoft.AspNetCore.HttpLogging
    logging.ResponseHeaders.Add("MyResponseHeader");
    //logging.MediaTypeOptions.AddText("application/javascript");
    //logging.RequestBodyLogLimit = 4096; // Log up to 4KB of the request body
    //logging.ResponseBodyLogLimit = 4096;
    //logging.CombineLogs = true;
});

// Configure logging to log to both console and file
var logFilePath = $"{Path.GetTempPath()}/Logs/http-requests.log";//"%TEMP%
builder.Logging.ClearProviders(); //Clear existing providers(if needed)
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
//builder.Logging.AddFile(logFilePath); // Requires Serilog.Extensions.Logging.File package
builder.Logging.AddProvider(new FileLoggerProvider(logFilePath));

//---------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage(); //use this, if swagger not used.
    app.UseSwagger();
    app.UseSwaggerUI();
}

//---------------------------------------------------
//Use HTTP Logging Middleware: Adds the middleware to the request pipeline to log the HTTP requests and responses. Keep app.UseHttpLogging() after the swagger pipeline.
app.UseHttpLogging();
//---------------------------------------------------

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//---------------------------------------------------
/*
// Set up logging
var logger = LoggerFactory.Create(builder =>
{
    builder.AddConsole()
           .AddProvider(new FileLoggerProvider($"{Path.GetTempPath()}/Logs/http-requests.log"));
}).CreateLogger("HTTPLogger");

// Middleware to log requests and responses
app.Use(async (context, next) =>
{
    logger.LogInformation($"HTTP Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    logger.LogInformation($"HTTP Response: {context.Response.StatusCode}");
});
*/
app.Use(async (context, next) =>
{
    context.Response.Headers["MyResponseHeader"] = new string[] { "My Response Header Value" };
    await next();
});

app.MapGet("/", () => "\n\nHello World! This is Http-Logging.");

//For endpoint-specific configuration in minimal API apps
app.MapGet("/response", () => "Hello World! (logging response)")
    .WithHttpLogging(HttpLoggingFields.ResponsePropertiesAndHeaders);

//For endpoint-specific configuration in apps that use controllers,
app.MapGet("/duration", [HttpLogging(loggingFields: HttpLoggingFields.Duration)]
() => "Hello World! (logging duration)");

//---------------------------------------------------

app.Run();

