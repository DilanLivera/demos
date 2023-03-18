using System.CommandLine;

namespace WeatherApp;

internal class ForecastCommand : Command
{
    private readonly FakeWeatherService _weather;

    internal ForecastCommand(FakeWeatherService weather)
        : base(name: "forecast", description: "Get the forecast. Almost always wrong.")
    {
        _weather = weather ?? throw new ArgumentNullException(nameof(weather));

        var cityOption = new Option<string>(
            name: "--city",
            getDefaultValue: () => _weather.Settings.DefaultCity,
            description: "The city.");
        var daysOption = new Option<int>(
            name: "--days",
            getDefaultValue: () => _weather.Settings.DefaultForecastDays,
            description: "Number of days.");

        AddOption(cityOption);
        AddOption(daysOption);

        this.SetHandler(Execute, cityOption, daysOption);
    }

    private async Task Execute(string city, int days)
    {
        var report = await _weather.Forecast(days, city);
        foreach (var item in report)
        {
            Console.WriteLine(item);
        }
    }
}
