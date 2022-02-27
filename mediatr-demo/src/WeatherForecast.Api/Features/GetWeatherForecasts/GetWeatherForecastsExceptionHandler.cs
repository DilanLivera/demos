using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using WeatherForecasts.Api.Models;

namespace WeatherForecasts.Api.Features.GetWeatherForecasts
{
    public class GetWeatherForecastsExceptionHandler
        : AsyncRequestExceptionHandler<GetWeatherForecastsCommand, IEnumerable<WeatherForecast>>
    {
        private readonly ILogger _logger;

        public GetWeatherForecastsExceptionHandler(
            ILogger<GetWeatherForecastsExceptionHandler> logger)
        {
            _logger = logger;
        }

        protected override Task Handle(
            GetWeatherForecastsCommand request,
            Exception exception,
            RequestExceptionHandlerState<IEnumerable<WeatherForecast>> state,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception,
                "Exception was thrown while handling {CommandName} command",
                nameof(GetWeatherForecastsCommand));

            return Task.CompletedTask;
        }
    }
}
