using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WeatherForecastUi.Authentication
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<Auth0> _auth0Options;

        public TokenService(HttpClient httpClient, IOptions<Auth0> auth0Options)
        {
            _httpClient = httpClient;
            _auth0Options = auth0Options;
        }

        public async Task<AccessToken> GetTokenAsync()
        {
            var tokenRequest = new TokenRequest
            {
                ClientId = _auth0Options.Value.ClientId,
                ClientSecret = _auth0Options.Value.ClientSecret,
                Audience = "https://localhost:5001/WeatherForecast",
                GrantType = "client_credentials"
            };

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/oauth/token");
            requestMessage.Content = JsonContent.Create(tokenRequest);
            requestMessage.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadFromJsonAsync<AccessToken>();

            return token;
        }
    }
}
