using Microsoft.AspNetCore.Components;

namespace WeatherApp.Components.Pages.WeatherForecasts;

public partial class Weather
{
    private WeatherForecast[]? _forecasts;

    [Inject] public required WeatherForecastClient WeatherForecastClient { get; set; }

    protected override async Task OnInitializedAsync() => _forecasts = await WeatherForecastClient
        .Value
        .GetFromJsonAsync<WeatherForecast[]>(
            requestUri: "/weatherforecasts",
            CancellationToken.None);
}
