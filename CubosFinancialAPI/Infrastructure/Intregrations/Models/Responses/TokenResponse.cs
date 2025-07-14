using System.Text.Json.Serialization;

namespace CubosFinancialAPI.Infrastructure.Intregrations.Models.Responses
{
    public class TokenResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public TokenData Data { get; set; }
    }

    public class TokenData
    {
        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
