using System.Text.Json.Serialization;

namespace Business.DTOs.Users;

public class UserDto
{
    
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; } = default!;
    
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = default!;
    
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = default!; 
    
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;
    
    [JsonPropertyName("emailVerified")]
    public bool EmailVerifed { get; set; } 
    
    [JsonPropertyName("createdTimestamp")]
    public long CreatedTimestamp { get; set; }
    
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}

