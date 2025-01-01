namespace Core.Services.ServiceOptions;

public sealed class KeycloakConfiguration
{
    public string HostName { get; set; } = default!;
    
    public string Realm { get; set; } = default!;
    
    public string ClientId { get; set; } = default!;

    public string ClientSecret { get; set; } = default!;
    
    public string ClientUUID { get; set; } = default!;
    
    
}