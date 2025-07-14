using System.Text.Json.Serialization;

namespace CubosFinancialAPI.Infrastructure.Intregrations.Models.Responses
{
    public class ValidationResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public ValidationData Data { get; set; }
    }

    public class ValidationData
    {
        [JsonPropertyName("document")]
        public string Document { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}