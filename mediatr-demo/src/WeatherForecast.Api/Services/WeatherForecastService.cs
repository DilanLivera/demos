using System;
using System.Collections.Generic;
using System.Linq;
using WeatherForecasts.Api.Models;

namespace WeatherForecasts.Api.Services
{
    public class WeatherForecastService
    {
        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            var rng = new Random();
            IEnumerable<WeatherForecast> weatherForecasts = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summaries[rng.Next(_summaries.Length)]
                });

            return weatherForecasts.ToList();
        }
    }
}
