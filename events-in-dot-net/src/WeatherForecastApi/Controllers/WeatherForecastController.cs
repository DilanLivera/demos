using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecastApi.Models;
using WeatherForecastApi.Repositories;
using WeatherForecastApi.Services;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            MessageService messageService,
            WeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
            _weatherForecastRepository.WeatherForecastFetched += async (sender, eventArgs) =>
            {
                try
                {
                    await messageService.OnWeatherForecastFetched(sender, eventArgs);
                }
                catch (Exception exception)
                {
                    logger.LogError(
                        exception,
                        "Async {TaskName} task failed.",
                        nameof(messageService.OnWeatherForecastFetched));
                }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return Ok(_weatherForecastRepository.GetAsync());
        }
    }
}
