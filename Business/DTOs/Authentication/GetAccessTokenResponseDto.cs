using System.Text.Json.Serialization;

namespace Business.DTOs.Authentication;

public sealed class GetAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
}