using Data_Access.Other_Objects;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("Get:Member-Access")]
        [Authorize(Roles = UserRoles.NEWMEMBER)]
        public IActionResult Get_Member()
        {
          return Ok(Summaries);
        }
        [HttpGet]
        [Route("Get:Admin-Access")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public IActionResult Get_Admin()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("Get:Magnet-Access")]
        [Authorize(Roles = UserRoles.MAGNET)]
        public IActionResult Get_Magnet()
        {
            return Ok(Summaries);
        }
    }
}
