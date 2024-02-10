namespace Weather.Forecasts.API.WeatherForecasts;

internal static class RouteEndpoints
{
    internal static void MapGetWeatherForecasts(this WebApplication app) => app
        .MapGet(
            pattern: "/weatherforecasts",
            handler: WeatherForecastsHandlers.GetWeatherForecastsHandler)
        .WithName("GetWeatherForecast")
        .WithOpenApi();
}
