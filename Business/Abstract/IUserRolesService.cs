using Business.DTOs.Roles;

namespace Business.Abstract;

public interface IUserRolesService
{
    Task<string> AssignmentRolesByUserId(Guid id , List<RoleDto> request,CancellationToken cancellationToken);
    
    Task<string> UnAssignmentRolesByUserId(Guid id , List<RoleDto> request,CancellationToken cancellationToken);
    
    Task<object> GetAllUsersRolesByUSerId(Guid id ,CancellationToken cancellationToken);


}