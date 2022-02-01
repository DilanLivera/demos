using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IQueryHandler<string, IEnumerable<WeatherForecast>> _queryHandler;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger, 
        IQueryHandler<string, IEnumerable<WeatherForecast>> queryHandler)
    {
        _logger = logger;
        _queryHandler = queryHandler;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync(CancellationToken cancellationToken)
    {
        return await _queryHandler.Handle("", cancellationToken);
    }
}
