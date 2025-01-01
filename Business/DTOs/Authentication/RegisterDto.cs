namespace Business.DTOs.Authentication;

public sealed record class RegisterDto(string UserName, string FirstName, string LastName, string Email, string Password)
{
    
}