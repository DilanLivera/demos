using MediatR;

namespace WeatherForecastApi.Controllers;

public class WeatherForecastQueryHandler : IQueryHandler<string, IEnumerable<WeatherForecast>>
{
    private static readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IEnumerable<WeatherForecast>> Handle(
        string query, CancellationToken cancellation)
    {
        var weatherForecasts = Enumerable
            .Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            });

        return Task.FromResult(weatherForecasts);
    }
}
