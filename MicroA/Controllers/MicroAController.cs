using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace MicroA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MicroAController : ControllerBase
    {

        private readonly ILogger<MicroAController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MicroAController(ILogger<MicroAController> logger
            , IHttpClientFactory httpClientFactory
            , IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        [HttpGet("servicea")]
        public async Task<IEnumerable<Response>> Get()
        {
            string base_url = _configuration.GetValue<string>("MicroServiceB") + "MicroB";

            HttpResponseMessage response = await _httpClient.GetAsync($"{base_url}/serviceb");

            if (response.IsSuccessStatusCode)
            {
                var output = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Response[]>(output)!;

                var outputvalue = res.ToList();
                outputvalue.Add(new Response { Data = "testing A", Source = "MicroA" });
                return outputvalue;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve data. Status code: {response.StatusCode}");
            }
        }
    }
}
