using Business.Abstract;
using Business.DTOs.Roles;
using Core.Services.ServiceOptions;
using Microsoft.Extensions.Options;

namespace Business.Concrete;

public class UserRolesManager: IUserRolesService
{
    private readonly IOptions<KeycloakConfiguration> _options;
    private readonly HttpClient _httpClient;
    private readonly IKeycloakService _keycloakService;
    
    public UserRolesManager(IOptions<KeycloakConfiguration> options, IKeycloakService keycloakService)
    {
        _options = options;
        _httpClient = new HttpClient();
        _keycloakService = keycloakService;
    }


    public async Task<string> AssignmentRolesByUserId(Guid id, List<RoleDto> request, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users/{id}/role-mappings/clients/{_options.Value.ClientUUID}";
        
        var response = await _keycloakService.PostAsync<string>(endpoint , request , true , cancellationToken );

        if (!response.IsSuccessful)
        {
            throw new Exception(response.ErrorMessages?.FirstOrDefault() ?? "Kullanıcının rolleri eklenemedi.");
        }
        
        return response.Data;
    }

    public async Task<string> UnAssignmentRolesByUserId(Guid id, List<RoleDto> request, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users/{id}/role-mappings/clients/{_options.Value.ClientUUID}";
        
        var response = await _keycloakService.DeleteAsync<string>(endpoint , request , true , cancellationToken );

        if (!response.IsSuccessful)
        {
            throw new Exception(response.ErrorMessages?.FirstOrDefault() ?? "Kullanıcının rolleri eklenemedi.");
        }
        
        return response.Data;
        
    }

    public async Task<object> GetAllUsersRolesByUSerId(Guid id, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users/{id}/role-mappings/clients/{_options.Value.ClientUUID}";
        
        var response = await _keycloakService.GetAsync<object>(endpoint, true , cancellationToken );
        
        return response.Data;
        
    }
}