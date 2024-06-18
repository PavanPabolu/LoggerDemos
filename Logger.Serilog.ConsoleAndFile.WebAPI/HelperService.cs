using Serilog;

namespace Logger.Serilog.ConsoleAndFile.WebAPI
{
    public class HelperService
    {
        private readonly ILogger<HelperService> _logger;

        public HelperService(ILogger<HelperService> logger)
        {
            _logger = logger;
        }

        public void PerformOperation()
        {
            _logger.LogInformation($"HelperService PerformOperation method called. ");
            Log.Information($"HelperService PerformOperation method called.");
        }
    }
}
