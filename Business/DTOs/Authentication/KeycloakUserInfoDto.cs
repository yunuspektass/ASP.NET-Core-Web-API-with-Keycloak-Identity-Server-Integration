using System.Text.Json.Serialization;

namespace Business.DTOs.Authentication;

public class KeycloakUserInfoDto
{
   
        [JsonPropertyName("sub")]
        public string Sub { get; set; }
    
        [JsonPropertyName("preferred_username")]
        public string PreferredUsername { get; set; }
    
        [JsonPropertyName("given_name")]
        public string GivenName { get; set; }
    
        [JsonPropertyName("family_name")]
        public string FamilyName { get; set; }
    
        [JsonPropertyName("email")]
        public string Email { get; set; }
    
        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }
    
        [JsonPropertyName("resource_access")]
        public Dictionary<string, KeycloakResourceAccess> ResourceAccess { get; set; 
    }
    
}

public class KeycloakResourceAccess
{
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = default!;
} 

