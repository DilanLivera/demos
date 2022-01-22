using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Proxy.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5003");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<WeatherForecast>>> GetAsync()
        {
            _logger.LogInformation("Getting weather forecasts");

            HttpResponseMessage response = await _httpClient.GetAsync("/WeatherForecast");

            response.EnsureSuccessStatusCode();

            Stream contentStream = await response.Content.ReadAsStreamAsync();

            IReadOnlyList<WeatherForecast> weatherForecasts = await JsonSerializer
                .DeserializeAsync<IReadOnlyList<WeatherForecast>>(contentStream);

            return Ok(weatherForecasts);
        }
    }
}
