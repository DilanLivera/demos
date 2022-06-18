using System.Text.Json.Serialization;

namespace WeatherForecastUi.Authentication
{
    public class AccessToken
    {
        [JsonPropertyName("access_token")]
        public string Value { get; init; }

        [JsonPropertyName("expires_in")]
        public int ExpiryTimeInMilliSeconds { get; set; }

        [JsonPropertyName("token_type")]
        public string Type { get; set; }
    }
}
