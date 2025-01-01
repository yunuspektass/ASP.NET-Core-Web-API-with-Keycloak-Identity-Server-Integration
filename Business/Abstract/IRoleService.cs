using Business.DTOs;
using Business.DTOs.Roles;

namespace Business.Abstract;

public interface IRoleService
{
    Task<List<RoleDto>> GetAll(CancellationToken cancellationToken);
    
    Task<RoleDto> GetByName(string name,CancellationToken cancellationToken);
    
    Task<RoleDto> DeleteByName(string name,CancellationToken cancellationToken);


    Task<string> Create(CreateRoleDto request,CancellationToken cancellationToken);


    
}