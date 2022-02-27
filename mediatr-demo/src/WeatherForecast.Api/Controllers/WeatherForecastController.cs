using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecasts.Api.Features.GetWeatherForecasts;
using WeatherForecasts.Api.Models;

namespace WeatherForecasts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            _logger.LogInformation("Getting weather forecasts");

            var command = new GetWeatherForecastsCommand();
            IEnumerable<WeatherForecast> forecasts = await _mediator.Send(command);

            _logger.LogInformation("Returning fetched weather forecasts");

            return Ok(forecasts);
        }
    }
}
