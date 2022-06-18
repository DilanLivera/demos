using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WeatherForecastUi.Authentication;

namespace WeatherForecastUi.Data
{
    public class WeatherForecastService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        public WeatherForecastService(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<WeatherForecast[]> GetForecastAsync()
        {
            AccessToken token = await _tokenService.GetTokenAsync();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast");
            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Value);

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();

            WeatherForecast[] forecasts =
                await response.Content.ReadFromJsonAsync<WeatherForecast[]>();

            return forecasts;
        }
    }
}
