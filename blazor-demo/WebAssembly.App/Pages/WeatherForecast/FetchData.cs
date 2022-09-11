using Microsoft.AspNetCore.Components;

namespace WebAssembly.App.Pages.WeatherForecast;

public partial class FetchData
{
    private IReadOnlyCollection<WeatherForecastViewModel>? Forecasts { get; set; }

    [Inject] public WeatherForecastService? ForecastService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (ForecastService is not null)
        {
            Forecasts = await ForecastService.GetWeatherForecasts();
        }
    }
}
