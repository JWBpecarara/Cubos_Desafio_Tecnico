using System.Text.Json.Serialization;

namespace CubosFinancialAPI.Infrastructure.Intregrations.Models.Responses
{
    public class AuthCodeResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public AuthCodeData Data { get; set; }
    }

    public class AuthCodeData
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("authCode")]
        public string AuthCode { get; set; }
    }
}