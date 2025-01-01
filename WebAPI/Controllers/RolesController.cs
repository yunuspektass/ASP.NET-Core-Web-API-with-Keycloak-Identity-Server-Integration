using Business.Abstract;
using Business.DTOs.Roles;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public sealed class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _roleService.GetAll(cancellationToken);

            if (roles == null || roles.Count == 0)
            {
                return NotFound(new { Message = "Tanımlı rolünüz bulunamamaktadır." });
            }
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleService.GetByName(name, cancellationToken);

            if (role == null)
            {
                return NotFound(new { Message = $"'{name}' adlı rolünüz bulunamamaktadır." });
            }
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleDto request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _roleService.Create(request, cancellationToken);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }
    
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleService.DeleteByName(name, cancellationToken);
            
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ModelState = ex.Message });
        }
    }
}