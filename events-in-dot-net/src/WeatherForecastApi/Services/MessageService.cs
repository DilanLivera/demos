using Microsoft.Extensions.Logging;
using WeatherForecastApi.Repositories;

namespace WeatherForecastApi.Services
{
    public class MessageService
    {
        private readonly ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public void OnWeatherForecastFetched(
            object source, WeatherForecastFetchedEventArgs eventArgs)
        {
            _logger.LogInformation(
                "MessageService: Sending message - Fetched {WeatherForecastsCount} weather forecasts.",
                eventArgs.WeatherForecasts.Count);
        }
    }
}
