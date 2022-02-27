using System.Collections.Generic;
using MediatR;
using WeatherForecasts.Api.Models;

namespace WeatherForecasts.Api.Features.GetWeatherForecasts
{
    public class GetWeatherForecastsCommand : IRequest<IEnumerable<WeatherForecast>>
    {
    }
}
