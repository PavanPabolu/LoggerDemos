namespace Logger.AzureApplicationInsight.Analytics.ClientSide.WebApp.Common
{
    public interface IAppConfigurationService
    {
        string GetConfigValue(string key);
    }

    public class AppConfigurationService : IAppConfigurationService
    {
        private readonly IConfiguration _configuration;

        public AppConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConfigValue(string key)
        {
            return _configuration[key];
        }
    }
}
/*
@using Microsoft.Extensions.Configuration
@inject IAppConfigurationService AppConfigurationService

<p>The value of 'MySettings:TestProperty' is: @AppConfigurationService.GetConfigValue("MySettings:TestProperty")</p>
 */
