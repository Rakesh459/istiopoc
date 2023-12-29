using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;

namespace MicrosServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GatewayController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;




        public GatewayController(ILogger<GatewayController> logger
            ,IHttpClientFactory httpClientFactory
            ,IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        [HttpGet(Name = "GetData")]
        public async Task<IEnumerable<Response>> Get()
        {
            string base_url = _configuration.GetValue<string>("MicroServiceA")+ "MicroA";

            HttpResponseMessage response = await _httpClient.GetAsync($"{base_url}/servicea");

            if (response.IsSuccessStatusCode)
            {
                var output = await response.Content.ReadAsStringAsync();
               return JsonConvert.DeserializeObject<Response[]>(output)!;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve data. Status code: {response.StatusCode}");
            }

        }
    }
}
