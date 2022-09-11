using System.Net.Http.Json;

namespace WebAssembly.App.Pages.WeatherForecast;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<WeatherForecastViewModel>> GetWeatherForecasts()
    {
        var forecasts = await _httpClient
            .GetFromJsonAsync<List<WeatherForecastViewModel>>("sample-data/weather.json");

        return forecasts is null
            ? Enumerable.Empty<WeatherForecastViewModel>().ToList().AsReadOnly()
            : forecasts.AsReadOnly();
    }
}
