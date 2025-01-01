using Business.Abstract;
using Business.DTOs.Roles;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController:ControllerBase
{
    private readonly IUserRolesService _userRolesService;
    
    public UserRolesController(IUserRolesService userRolesService)
    {
        _userRolesService = userRolesService;
    }
    
    [HttpPost("{id}/roles")]
    public async Task<IActionResult> AssignmentRolesByUserId(Guid id, List<RoleDto> request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userRolesService.AssignmentRolesByUserId(id, request, cancellationToken);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }
    
    [HttpDelete("{id}/roles")]
    public async Task<IActionResult> UnAssignmentRolesByUserId(Guid id, List<RoleDto> request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userRolesService.UnAssignmentRolesByUserId(id, request, cancellationToken);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }
    
    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetAllUsersRolesByUserId(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userRolesService.GetAllUsersRolesByUSerId(id, cancellationToken);

            if (response == null)
            {
                return NotFound(new  { Message = "Kullanıcıya ait rol bulunamadı."});
            }
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }
    
}