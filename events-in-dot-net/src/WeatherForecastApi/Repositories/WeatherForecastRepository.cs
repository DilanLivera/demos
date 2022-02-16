using System;
using System.Collections.Generic;
using System.Linq;
using WeatherForecastApi.Models;

namespace WeatherForecastApi.Repositories
{
    public class WeatherForecastFetchedEventArgs : EventArgs
    {
        public IReadOnlyList<WeatherForecast> WeatherForecasts { get; set; }
    }

    public class WeatherForecastRepository
    {
        public event EventHandler<WeatherForecastFetchedEventArgs> WeatherForecastFetched;
        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IReadOnlyList<WeatherForecast> GetAsync()
        {
            var rng = new Random();
            var weatherForecasts = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summaries[rng.Next(_summaries.Length)]
                })
                .ToList();

            OnWeatherForecastFetched(weatherForecasts);

            return weatherForecasts;
        }

        protected virtual void OnWeatherForecastFetched(IReadOnlyList<WeatherForecast> weatherForecasts)
        {
            if (WeatherForecastFetched is null)
            {
                return;
            }

            var weatherForecastFetchedEventArgs = new WeatherForecastFetchedEventArgs
            {
                WeatherForecasts = weatherForecasts
            };

            WeatherForecastFetched(this, weatherForecastFetchedEventArgs);
        }
    }
}
