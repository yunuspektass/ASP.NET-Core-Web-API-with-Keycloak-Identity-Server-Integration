using Business.Abstract;
using Business.DTOs;
using Business.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize("UserGetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userService.GetAll(cancellationToken);

            if (users == null || !users.Any())
            {
                return NotFound(new { Message = "Tanımlı kullanıcınız bulunamamaktadır." });
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("by-email")]
    [Authorize("UserGetAll")]
    public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userService.GetByEmail(email, cancellationToken);

            if (users == null || !users.Any())
            {
                return NotFound(new { Message = $"'{email}' email adresine sahip kullanıcı bulanamadı." });
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("by-username")]
    [Authorize("UserGetAll")]
    public async Task<IActionResult> GetByUserName(string userName, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userService.GetByUserName(userName, cancellationToken);

            if (users == null || !users.Any())
            {
                return NotFound(new { Message = $"'{userName}' isimli kullanıcı bulanamadı." });
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    [Authorize("UserGetAll")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetById(id, cancellationToken);

            if (user == null)
            {
                return NotFound(new { Message = $"'{id}' numaralı kullanıcı bulanamadı." });
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    [Authorize("UserUpdate")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.Update(id, request, cancellationToken);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize("UserDelete")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.DeleteById(id, cancellationToken);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}