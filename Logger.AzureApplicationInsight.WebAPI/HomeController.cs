using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.ApplicationInsights;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logger.AzureApplicationInsight.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            
            _logger.LogInformation("This is test message from Logger.AzureApplication.WebAPI controller - {DT}.", DateTime.UtcNow.ToLongTimeString());
            _logger.LogWarning("An example of a Warning trace..");
            _logger.LogError("An example of an Error level message");
            _logger.LogTrace("An example of an Trace level message");






            return new string[] { "value1", "value2" };
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
