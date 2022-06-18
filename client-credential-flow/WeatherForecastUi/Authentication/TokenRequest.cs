using System.Text.Json.Serialization;

namespace WeatherForecastUi.Authentication
{
    public class TokenRequest
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; init; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; init; }

        [JsonPropertyName("audience")]
        public string Audience { get; init; }

        [JsonPropertyName("grant_type")]
        public string GrantType { get; init; }
    }
}
