using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Models;
using WeatherForecastApi.Services;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastController(
            MessageService messageService,
            WeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
            _weatherForecastRepository.WeatherForecastFetched += messageService.OnWeatherForecastFetched;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return Ok(_weatherForecastRepository.GetAsync());
        }
    }
}
