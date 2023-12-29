using Microsoft.AspNetCore.Mvc;

namespace MicroB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MicroBController : ControllerBase
    {

        private readonly ILogger<MicroBController> _logger;

        public MicroBController(ILogger<MicroBController> logger)
        {
            _logger = logger;
        }

        [HttpGet("serviceb")]
        public object Get()
        {
            return new[] { new { Data = "testing B", Source = "MicroB" } };
        }
    }
}
