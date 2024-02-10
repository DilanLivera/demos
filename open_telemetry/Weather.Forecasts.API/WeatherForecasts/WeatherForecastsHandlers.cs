namespace Weather.Forecasts.API.WeatherForecasts;

internal static class WeatherForecastsHandlers
{
    internal static readonly Func<WeatherForecast[]> GetWeatherForecastsHandler = () => Enumerable
        .Range(1, 5)
        .Select(index => new WeatherForecast
        (
            Date: DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC: Random.Shared.Next(-20, 55),
            Summary: WeatherForecastsSummaries.Value.ElementAt(
                Random.Shared.Next(WeatherForecastsSummaries.Value.Count))))
        .ToArray();
}
