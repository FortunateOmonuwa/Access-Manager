using Microsoft.AspNetCore.Mvc;

namespace Access_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];


        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
          return Ok(Summaries);
        }
    }
}