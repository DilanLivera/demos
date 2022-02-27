using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecasts.Api.Models;
using WeatherForecasts.Api.Services;

namespace WeatherForecasts.Api.Features.GetWeatherForecasts
{
    public class GetWeatherForecastsCommandHandler
        : IRequestHandler<GetWeatherForecastsCommand, IEnumerable<WeatherForecast>>
    {
        private readonly IMediator _mediator;
        private readonly WeatherForecastService _weatherForecastService;
        private const bool SimulateException = false;

        public GetWeatherForecastsCommandHandler(
            IMediator mediator,
            WeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
            _mediator = mediator;
        }

        public Task<IEnumerable<WeatherForecast>> Handle(
            GetWeatherForecastsCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<WeatherForecast> forecasts = _weatherForecastService.GetWeatherForecasts();

            if (SimulateException)
            {
                throw new ApplicationException("Oops, something went wrong");
            }

            _mediator.Publish(new WeatherForecastsFetchedNotification(), cancellationToken);

            return Task.FromResult(forecasts);
        }
    }
}
