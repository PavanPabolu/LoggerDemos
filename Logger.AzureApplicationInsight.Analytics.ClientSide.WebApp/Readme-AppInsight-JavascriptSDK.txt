

Enable Azure Monitor Application Insights Real User Monitoring

-------------------------------------------------------------------------
Enable client-side: Using Microsoft.ApplicationInsights.AspNetCore at _ViewImports.cshtml
https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core?tabs=netcorenew#enable-client-side-telemetry-for-web-applications

Enable client-side: Using Javascript SDK at _layout page:
https://learn.microsoft.com/en-us/azure/azure-monitor/app/javascript-sdk?tabs=javascriptwebsdkloaderscript
-------------------------------------------------------------------------

To collect clicks by default, consider adding the Click Analytics Auto-Collection plug-in:
https://learn.microsoft.com/en-us/azure/azure-monitor/app/javascript-feature-extensions


JavaScript (Web) SDK Loader Script:
For most customers, we recommend the JavaScript (Web) SDK Loader Script because you never have to update 
the SDK and you get the latest updates automatically. Also, you have control over which pages you add the 
Application Insights JavaScript SDK to.

npm package: 
You want to bring the SDK into your code and enable IntelliSense. This option is only needed for developers 
who require more custom events and configuration. This method is required if you plan to use the React, 
React Native, or Angular Framework Extension.




