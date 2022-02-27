using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WeatherForecasts.Api.Features.GetWeatherForecasts
{
    public class WeatherForecastsFetchedNotificationHandler
        : INotificationHandler<WeatherForecastsFetchedNotification>
    {
        private readonly ILogger _logger;

        public WeatherForecastsFetchedNotificationHandler(
            ILogger<WeatherForecastsFetchedNotificationHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(
            WeatherForecastsFetchedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Weather forecasts got fetched");
            return Task.CompletedTask;
        }
    }
}
