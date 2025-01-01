using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;
using Business.Abstract;
using Business.DTOs;
using Business.DTOs.Authentication;
using Business.DTOs.Common;
using Business.DTOs.Roles;
using Business.DTOs.Users;
using Core.Services.ServiceOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TS.Result;
using IResult = Microsoft.AspNetCore.Http.IResult;

public class KeycloakManager : IKeycloakService
{
    private readonly IOptions<KeycloakConfiguration> _options;
    private readonly HttpClient _httpClient;

    public KeycloakManager(IOptions<KeycloakConfiguration> options)
    {
        _options = options;
        _httpClient = new HttpClient();
    }

    public async Task<string> GetAccessToken(CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/realms/{_options.Value.Realm}/protocol/openid-connect/token";

        List<KeyValuePair<string, string>> data = new()
        {
            new("grant_type", "client_credentials"),
            new("client_id", _options.Value.ClientId),
            new("client_secret", _options.Value.ClientSecret)
        };


        Result<GetAccessTokenResponseDto> result =
            await PostUrlEncodedFormAsync<GetAccessTokenResponseDto>(endpoint, data, false, cancellationToken);

        return result.Data!.AccessToken;
    }

    public async Task<(bool IsSuccess, string Message)> Register(RegisterDto request,
        CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/admin/realms/{_options.Value.Realm}/users";

        var data = new
        {
            username = request.UserName,
            firstName = request.FirstName,
            lastName = request.LastName,
            email = request.Email,
            enabled = true,
            emailVerified = true,
            credentials = new List<object>
            {
                new
                {
                    type = "password",
                    value = request.Password,
                    temporary = false
                }
            }
        };

        var response = await PostAsync<string>(endpoint, data, true, cancellationToken);

        return response.IsSuccessful
            ? (true, "User registered successfully")
            : (false, response.ErrorMessages?.FirstOrDefault() ?? "An error occurred during registration");
    }

    public async Task<(bool IsSuccess, string Message)> Login(LoginDto request, CancellationToken cancellationToken)
    {
        string endpoint = $"{_options.Value.HostName}/realms/{_options.Value.Realm}/protocol/openid-connect/token";

        List<KeyValuePair<string, string>> data = new()
        {
            new("grant_type", "password"),
            new("client_id", _options.Value.ClientId),
            new("client_secret", _options.Value.ClientSecret),
            new("username", request.UserName),
            new("password", request.Password)
        };

        Result<GetAccessTokenResponseDto> response =
            await PostUrlEncodedFormAsync<GetAccessTokenResponseDto>(endpoint, data, false,
                cancellationToken);

        return response.IsSuccessful
            ? (true, response.Data!.AccessToken)
            : (false, response.ErrorMessages?.FirstOrDefault() ?? "An error occurred during login");
    }

    public async Task<Result<T>> PostAsync<T>(string endpoint, object data, bool reqToken = false,
        CancellationToken cancellationToken = default)
    {
        if (reqToken)
        {
            string token = await GetAccessToken(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }

        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");

        var message = await _httpClient.PostAsync(endpoint, content, cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            return Result<T>.Failure(errorResultForOther!.ErrorMessage);
        }

        if (message.StatusCode == HttpStatusCode.Created || message.StatusCode == HttpStatusCode.NoContent)
        {
            return Result<T>.Succeed(default!);
        }

        var obj = JsonSerializer.Deserialize<T>(response);

        return Result<T>.Succeed(obj!);
    }

    public async Task<Result<T>> PutAsync<T>(string endpoint, object data, bool reqToken = false, CancellationToken cancellationToken = default)
    {
        if (reqToken)
        {
            string token = await GetAccessToken(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }

        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");

        var message = await _httpClient.PutAsync(endpoint, content, cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            return Result<T>.Failure(errorResultForOther!.ErrorMessage);
        }

        if (message.StatusCode == HttpStatusCode.Created || message.StatusCode == HttpStatusCode.NoContent)
        {
            return Result<T>.Succeed(default!);
        }

        var obj = JsonSerializer.Deserialize<T>(response);

        return Result<T>.Succeed(obj!);    }

    public async Task<Result<T>> DeleteAsync<T>(string endpoint, bool reqToken = false,
        CancellationToken cancellationToken = default)
    {

        
        
        if (reqToken)
        {
            string token = await GetAccessToken(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }
        

        var message = await _httpClient.DeleteAsync(endpoint, cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            return Result<T>.Failure(errorResultForOther!.ErrorMessage);
        }

        if (message.StatusCode == HttpStatusCode.Created || message.StatusCode == HttpStatusCode.NoContent)
        {
            return Result<T>.Succeed(default!);
        }

        var obj = JsonSerializer.Deserialize<T>(response);

        return Result<T>.Succeed(obj!);    }

    public async Task<Result<T>> DeleteAsync<T>(string endpoint, object data, bool reqToken = false, CancellationToken cancellationToken = default)
    {
      
        if (reqToken)
        {
            string token = await GetAccessToken(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }


        var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            
            string str = JsonSerializer.Serialize(data);
            request.Content = new StringContent(str, Encoding.UTF8, "application/json");
        
        
        var message = await _httpClient.SendAsync(request, cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            return Result<T>.Failure(errorResultForOther!.ErrorMessage);
        }

        if (message.StatusCode == HttpStatusCode.Created || message.StatusCode == HttpStatusCode.NoContent)
        {
            return Result<T>.Succeed(default!);
        }

        var obj = JsonSerializer.Deserialize<T>(response);

        return Result<T>.Succeed(obj!);      }

    public async Task<Result<T>> PostUrlEncodedFormAsync<T>(string endpoint, List<KeyValuePair<string, string>> data,
        bool reqToken = false,
        CancellationToken cancellationToken = default)
    {
        if (reqToken)
        {
            string token = await GetAccessToken(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }


        var message = await _httpClient.PostAsync(endpoint, new FormUrlEncodedContent(data), cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }
            else if (message.StatusCode == HttpStatusCode.Unauthorized)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            return Result<T>.Failure(errorResultForOther!.ErrorMessage);
        }

        if (message.StatusCode == HttpStatusCode.Created || message.StatusCode == HttpStatusCode.NoContent)
        {
            return Result<T>.Succeed(default!);
        }

        var obj = JsonSerializer.Deserialize<T>(response);

        return Result<T>.Succeed(obj!);
    }

    public async Task<Result<T>> GetAsync<T>(string endpoint, bool reqToken = false, CancellationToken cancellationToken = default)
    {
        if (reqToken)
        {
            string token = await GetAccessToken(cancellationToken);

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }
        
        var message = await _httpClient.GetAsync(endpoint, cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                return Result<T>.Failure(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);

            return Result<T>.Failure(errorResultForOther!.ErrorMessage);
        }

        if (message.StatusCode == HttpStatusCode.Created || message.StatusCode == HttpStatusCode.NoContent)
        {
            return Result<T>.Succeed(default!);
        }

        var obj = JsonSerializer.Deserialize<T>(response);

        return Result<T>.Succeed(obj!);
    }

      
    public async Task<UserAuthInfoDto> GetCurrentUserInfoAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        try 
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new Exception("Invalid token format");
            }

            var userDto = new UserDto
            {
                Id = Guid.Parse(jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? string.Empty),
                Username = jsonToken.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value,
                FirstName = jsonToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value,
                LastName = jsonToken.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value,
                Email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                EmailVerifed = bool.Parse(jsonToken.Claims.FirstOrDefault(c => c.Type == "email_verified")?.Value ?? "false"),
                Enabled = true
            };

            var resourceAccess = jsonToken.Claims
                .FirstOrDefault(c => c.Type == "resource_access")?
                .Value;

            var roles = new List<RoleDto>();
            
            if (!string.IsNullOrEmpty(resourceAccess))
            {
                var resourceAccessObj = JsonSerializer.Deserialize<Dictionary<string, KeycloakResourceAccess>>(resourceAccess);
                roles = resourceAccessObj?["web-client"]?.Roles?
                    .Select(r => new RoleDto { Name = r })
                    .ToList() ?? new List<RoleDto>();
            }

            return new UserAuthInfoDto
            {
                User = userDto,
                Roles = roles,
                AccessToken = accessToken
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parsing user info from token: {ex.Message}", ex);
        }
    }
}