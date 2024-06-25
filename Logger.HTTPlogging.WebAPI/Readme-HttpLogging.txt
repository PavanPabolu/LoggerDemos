
Nuget:
Microsoft.Extensions.Logging
-----------------------------------------------------------------------------------------------
HTTP logging:
HTTP logging is a middleware that logs information about incoming HTTP requests and HTTP responses. HTTP logging provides logs of:
-HTTP request information
-Common properties
-Headers
-Body
-HTTP response information

HTTP logging can:
-Log all requests and responses or only requests and responses that meet certain criteria.
-Select which parts of the request and response are logged.
-Allow you to redact sensitive information from the logs.

HTTP logging can reduce the performance of an app, especially when logging the request and response bodies. Consider the performance impact when selecting fields to log. Test the performance impact of the selected logging properties.



-----------------------------------------------------------------------------------------------
HTTP logging in ASP.NET Core
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-8.0

To log HTTP requests and responses into a file without using Serilog, 
you can use the built-in FileLoggerProvider available in the Microsoft.Extensions.Logging namespace. 
This involves creating a custom logging provider to handle file logging. Below is an example of 
how to achieve this in ASP.NET Core 8.





