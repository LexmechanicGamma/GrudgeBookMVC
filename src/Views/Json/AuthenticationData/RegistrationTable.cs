using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GrudgeBookMvc.src.Views.Json.AuthenticationData
{
    public record struct RegistrationTable
    {
        [JsonInclude]
        [JsonPropertyName("email")]
        [AllowNull]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonInclude]
        [JsonPropertyName("username")]
        [Required]
        public required string UserName { get; set; }

        [JsonInclude]
        [JsonPropertyName("password")]
        [Required]
        public required string Password { get; set; }
    }
}
