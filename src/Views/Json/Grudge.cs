using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GrudgeBookMvc.src.Views.Json
{
    public record struct Grudge
    {
        [JsonInclude]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonInclude]
        [JsonPropertyName("titleOfSin")]
        public required string TitleOfSin { get; set; }

        [JsonInclude]
        [JsonPropertyName("timestamp")]
        public required string Timestamp { get; set; }

        [JsonInclude]
        [JsonPropertyName("condition")]
        public string Condition { get; set; }

        [JsonInclude]
        [JsonPropertyName("details")]
        public required string Details { get; set; }

        [JsonInclude]
        [JsonPropertyName("status")]

        public required string Status { get; set; }

        [JsonInclude]
        [JsonPropertyName("uri")]
        [AllowNull]
        public string? VisualizationURI { get; set; }
    }

    public record struct ErrorResponse
    {
        [JsonInclude]
        [JsonPropertyName("error")]
        public required string Error { get; set; }
    }
}