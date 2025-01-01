using Business.Abstract;
using Business.DTOs;
using Business.DTOs.Users;
using Core.Services.ServiceOptions;
using Microsoft.Extensions.Options;
using TS.Result;

namespace Business.Concrete;

public class UserManager: IUserService
{
    private readonly IOptions<KeycloakConfiguration> _options;
    private readonly HttpClient _httpClient;
    private readonly IKeycloakService _keycloakService;

    public UserManager(IOptions<KeycloakConfiguration> options, IKeycloakService keycloakService)
    {
        _options = options;
        _httpClient = new HttpClient();
        _keycloakService = keycloakService;
    }
    
    
    
    public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users";
        
        var response = await _keycloakService.GetAsync<List<UserDto>>(endpoint , true , cancellationToken);

        if (response.Data == null)
            throw new Exception("Kullanıcı listesi boş");
        
        return response.Data;

    }

    public async Task<List<UserDto>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users?email={email}";
        
        var response = await _keycloakService.GetAsync<List<UserDto>>(endpoint , true , cancellationToken);

        if (response.Data == null)
            throw new Exception("Kullanıcı listesi boş");
        
        return response.Data;
    }

    public async Task<List<UserDto>> GetByUserName(string userName, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users?username={userName}";
        
        var response = await _keycloakService.GetAsync<List<UserDto>>(endpoint , true , cancellationToken);

        if (response.Data == null)
            throw new Exception($"'{userName}' isimli kullanıcı bulunamadı.");
        
        return response.Data;
        
    }

    public async Task<UserDto> GetById(Guid id, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users/{id}";
        
        var response = await _keycloakService.GetAsync<UserDto>(endpoint , true , cancellationToken);

        if (response.Data == null)
            throw new Exception($"'{id}' numaralı kullanıcı bulunamadı.");
        
        return response.Data;
        
    }

    
    public async Task<string> Update(Guid id, UpdateUserDto request, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users/{id}";

        var result = await _keycloakService.PutAsync<string>(endpoint, request, true, cancellationToken);

        if (!result.IsSuccessful)
        {
            throw new Exception(result.ErrorMessages?.FirstOrDefault() ?? "Kullanıcı güncellenemedi.");
        }
    
        return result.Data;
    }

    public async Task<string> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users/{id}";

        var result = await _keycloakService.DeleteAsync<string>(endpoint, true, cancellationToken);

        if (!result.IsSuccessful)
        {
            throw new Exception(result.ErrorMessages?.FirstOrDefault() ?? "Kullanıcı Silinemedi.");
        }
    
        return result.Data;
        
    }
}