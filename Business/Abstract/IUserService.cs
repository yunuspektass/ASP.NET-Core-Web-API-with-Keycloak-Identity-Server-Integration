using Business.DTOs;
using Business.DTOs.Users;

namespace Business.Abstract;

public interface IUserService
{
    Task<List<UserDto>> GetAll(CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetByEmail (string email ,CancellationToken cancellationToken);

    Task<List<UserDto>> GetByUserName (string userName ,CancellationToken cancellationToken);
    
    Task<UserDto> GetById (Guid id ,CancellationToken cancellationToken);
    
    Task<string> Update(Guid id ,UpdateUserDto request, CancellationToken cancellationToken);
    
    Task<string> DeleteById(Guid id, CancellationToken cancellationToken);
}