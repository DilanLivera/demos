using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastController(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetAsync(
        CancellationToken cancellationToken)
    {
        var weatherForecasts = await _weatherForecastRepository.GetAsync(cancellationToken);

        var weatherForecastDtos = weatherForecasts.Select(weatherForecast => new WeatherForecastDto
        {
            Date = weatherForecast.Date,
            TemperatureC = weatherForecast.TemperatureC,
            TemperatureF = weatherForecast.TemperatureF,
            Summary = weatherForecast.Summary
        })
        .ToList();

        return Ok(weatherForecastDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WeatherForecastDto>> GetAsync(
        [FromRoute] string id, CancellationToken cancellationToken)
    {
        var weatherForecast = await _weatherForecastRepository.GetAsync(id, cancellationToken);

        var weatherForecastDto = new WeatherForecastDto
        {
            Date = weatherForecast.Date,
            TemperatureC = weatherForecast.TemperatureC,
            TemperatureF = weatherForecast.TemperatureF,
            Summary = weatherForecast.Summary
        };

        return Ok(weatherForecastDto);
    }

    [HttpPost]
    public async Task<ActionResult<WeatherForecastDto>> PostAsync(
        [FromBody] WeatherForecastDto weatherForecastDto, CancellationToken cancellationToken)
    {
        var weatherForecast = new WeatherForecast(
            weatherForecastDto.Date, weatherForecastDto.TemperatureC, weatherForecastDto.Summary);

        await _weatherForecastRepository.AddAsync(weatherForecast, cancellationToken);

        return Created("", null);
    }
}
