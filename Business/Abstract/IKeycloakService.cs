using Business.DTOs;
using Business.DTOs.Authentication;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace Business.Abstract;

public interface IKeycloakService
{
    Task<string> GetAccessToken(CancellationToken cancellationToken);
    Task<(bool IsSuccess, string Message)> Register(RegisterDto request, CancellationToken cancellationToken);

    Task<(bool IsSuccess, string Message)> Login(LoginDto request, CancellationToken cancellationToken);

    
    Task<Result<T>> PostAsync<T>(
        string endpoint,
        object data,
        bool reqToken = false,
        CancellationToken cancellationToken = default);
    
    Task<Result<T>> PutAsync<T>(
        string endpoint,
        object data,
        bool reqToken = false,
        CancellationToken cancellationToken = default);
    
    Task<Result<T>> DeleteAsync<T>(
        string endpoint,
        bool reqToken = false,
        CancellationToken cancellationToken = default);
    
    Task<Result<T>> DeleteAsync<T>(
        string endpoint,
        object data,
        bool reqToken = false,
        CancellationToken cancellationToken = default);

    Task<Result<T>> PostUrlEncodedFormAsync<T>(
        string endpoint,
        List<KeyValuePair<string, string>> data,
        bool reqToken = false,
        CancellationToken cancellationToken = default);
    
    Task<Result<T>> GetAsync<T>(
        string endpoint,
        bool reqToken = false,
        CancellationToken cancellationToken = default);
    
    Task<UserAuthInfoDto> GetCurrentUserInfoAsync(string accessToken, CancellationToken cancellationToken = default);
}