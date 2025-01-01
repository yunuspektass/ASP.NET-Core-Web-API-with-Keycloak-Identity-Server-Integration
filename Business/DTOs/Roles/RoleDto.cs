using System.Text.Json.Serialization;

namespace Business.DTOs.Roles;

public sealed class RoleDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = default!;



}