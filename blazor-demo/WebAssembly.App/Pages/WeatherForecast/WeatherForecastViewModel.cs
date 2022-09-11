namespace WebAssembly.App.Pages.WeatherForecast;

public class WeatherForecastViewModel
{
    public DateTime Date { get; init; }
    public int TemperatureC { get; init; }
    public string Summary { get; init; } = "";
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
