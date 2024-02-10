using Microsoft.Extensions.Options;

namespace WeatherApp.Components.Pages.WeatherForecasts;

internal static class WeatherForecastsServiceRegistration
{
    internal static IServiceCollection AddWeatherForecasts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<WeatherForecastClientConfiguration>()
            .Bind(configuration.GetSection(WeatherForecastClientConfiguration.SectionName));

        services.AddHttpClient<WeatherForecastClient>((serviceProvider, client) =>
        {
            var config = serviceProvider
                .GetRequiredService<IOptions<WeatherForecastClientConfiguration>>()
                .Value;
            client.BaseAddress = new Uri(config.BaseAddress);
        });

        return services;
    }
}
