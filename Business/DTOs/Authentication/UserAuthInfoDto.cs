using Business.DTOs.Roles;
using Business.DTOs.Users;

namespace Business.DTOs.Authentication;

public class UserAuthInfoDto
{
    public UserDto User { get; set; } = default!;
    public List<RoleDto> Roles { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
}