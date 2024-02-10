namespace Weather.Forecasts.API.WeatherForecasts;

internal static class WeatherForecastsSummaries
{
    private static readonly string[] _summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
        "Scorching"
    };

    internal static IReadOnlyCollection<string> Value => _summaries.AsReadOnly();
}
