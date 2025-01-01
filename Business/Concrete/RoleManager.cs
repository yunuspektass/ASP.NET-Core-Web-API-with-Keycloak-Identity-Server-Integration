using Business.Abstract;
using Business.DTOs;
using Business.DTOs.Roles;
using Core.Services.ServiceOptions;
using Microsoft.Extensions.Options;

namespace Business.Concrete;

public class RoleManager: IRoleService
{
    private readonly IOptions<KeycloakConfiguration> _options;
    private readonly HttpClient _httpClient;
    private readonly IKeycloakService _keycloakService;
    
    public RoleManager(IOptions<KeycloakConfiguration> options, IKeycloakService keycloakService)
    {
        _options = options;
        _httpClient = new HttpClient();
        _keycloakService = keycloakService;
    }


    public async Task<List<RoleDto>> GetAll(CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/clients/{_options.Value.ClientUUID}/roles";
        
        var response = await _keycloakService.GetAsync<List<RoleDto>>(endpoint , true , cancellationToken);

        if (response.Data == null)
            throw new Exception("Kullanıcı rol listesi boş");
        
        return response.Data;
    }

    public async Task<RoleDto> GetByName(string name, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/clients/{_options.Value.ClientUUID}/roles/{name}";;
        
        var response = await _keycloakService.GetAsync<RoleDto>(endpoint , true , cancellationToken);

        if (response.Data == null)
            throw new Exception("Kullanıcı rolü bulunamadı");
        
        return response.Data;
        
    }

    public async Task<RoleDto> DeleteByName(string name, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/clients/{_options.Value.ClientUUID}/roles/{name}";;
        
        var response = await _keycloakService.DeleteAsync<RoleDto>(endpoint , true , cancellationToken);
        
        
        if (!response.IsSuccessful)
        {
            throw new Exception(response.ErrorMessages?.FirstOrDefault() ?? "Kullanıcı rolü silinemedi.");
        }
        
        return response.Data;
        
    }

    public async Task<string> Create(CreateRoleDto request, CancellationToken cancellationToken)
    {

        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/clients/{_options.Value.ClientUUID}/roles";;
        
        var response = await _keycloakService.PostAsync<string>(endpoint , request , true , cancellationToken );

        if (!response.IsSuccessful)
        {
            throw new Exception(response.ErrorMessages?.FirstOrDefault() ?? "Kullanıcı rolü eklenemedi.");
        }
        
        return response.Data;
        
    }
}