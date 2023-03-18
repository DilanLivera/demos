using System.CommandLine;

namespace WeatherApp;

internal class CurrentCommand : Command
{
    private readonly FakeWeatherService _weather;

    internal CurrentCommand(FakeWeatherService weather)
        : base(name: "current", description: "Gets the current temperature.")
    {
        _weather = weather ?? throw new ArgumentNullException(nameof(weather));

        var cityOption = new Option<string>(
            name: "--city",
            getDefaultValue: () => _weather.Settings.DefaultCity,
            description: "The city.");

        AddOption(cityOption);

        this.SetHandler(Execute, cityOption);
    }

    private async Task Execute(string city)
    {
        var report = await _weather.GetTemperature(city);
        Console.WriteLine(report);
    }
}
