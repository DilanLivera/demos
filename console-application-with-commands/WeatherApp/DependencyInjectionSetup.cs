using System.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp;

internal static class DependencyInjectionSetup
{
    internal static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        // build config
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        // settings
        services.Configure<FakeWeatherServiceSettings>(configuration.GetSection(key: "Weather"));

        // add commands:
        services.AddTransient<Command, CurrentCommand>();
        services.AddTransient<Command, ForecastCommand>();

        // add services:
        services.AddTransient<FakeWeatherService>();

        return services;
    }
}
