namespace WeatherApp.Components.Pages.WeatherForecasts;

public sealed class WeatherForecastClient(HttpClient httpClient)
{
    public HttpClient Value { get; } = httpClient;
}
