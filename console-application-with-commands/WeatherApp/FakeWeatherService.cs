using Microsoft.Extensions.Options;

namespace WeatherApp;

internal class FakeWeatherServiceSettings
{
    public string DefaultCity { get; set; } = "Zwolle, NLD";

    public int DefaultForecastDays { get; set; } = 5;
}

internal class FakeWeatherService
{
    public FakeWeatherService(IOptions<FakeWeatherServiceSettings> settings)
    {
        Settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    internal FakeWeatherServiceSettings Settings { get; }

    internal Task<string> GetTemperature(string city = "")
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            city = Settings.DefaultCity;
        }

        var report = $"In {city} it is now {Random.Shared.Next(-20, 40)} degrees celcius.";
        return Task.FromResult(report);
    }

    internal Task<string[]> Forecast(int days, string city = "")
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            city = Settings.DefaultCity;
        }

        var reports = new List<string> { $"Report for {city} for the next {days} days:" };

        for (var i = 0; i < days; i++)
        {
            var date = DateTime.Now.AddDays(i + 1).ToString("yyyy-MM-dd");
            var report = $"- {date}: {Random.Shared.Next(-20, 40),3} degrees celcius.";
            reports.Add(report);
        }

        return Task.FromResult(reports.ToArray());
    }
}
