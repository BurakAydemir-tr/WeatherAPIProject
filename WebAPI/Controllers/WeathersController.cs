using Business.OpenWeatherServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeathersController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;

        public WeathersController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet("GetCurrentWeatherByLocation")]
        public IActionResult GetCurrentWeatherByLocation(string city)
        {
            var result= _openWeatherService.GetCoordinateByCity(city);
            if (result!=null)
            {
                var weatherData = _openWeatherService.GetCurrentWeatherByLocation(result.Result[0].lat,
                    result.Result[0].lon);
                return Ok(weatherData);
            }
            return BadRequest("Konum bilgisine ulaşılamadı.");
        }
    }
}
