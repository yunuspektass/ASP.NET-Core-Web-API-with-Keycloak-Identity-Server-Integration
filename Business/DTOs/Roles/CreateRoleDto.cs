using System.Text.Json.Serialization;

namespace Business.DTOs.Roles;

public sealed class CreateRoleDto
{
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}