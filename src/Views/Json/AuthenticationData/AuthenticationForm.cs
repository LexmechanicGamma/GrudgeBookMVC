
using System.Text.Json.Serialization;

namespace GrudgeBookMvc.src.Views.Json.AuthenticationData
{
    public record struct AuthenticationForm
    {
        [JsonInclude]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonInclude]
        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }

    public record struct AuthErrorResponse
    {
        [JsonInclude]
        [JsonPropertyName("error")]
        public required string Error { get; set; }
    }
}
