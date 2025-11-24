using System.ComponentModel.DataAnnotations;

namespace FlowershopAPI.Contracts.Authentication;

public class RegisterAccountRequest
{
    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
    
    [Required]
    public required string PasswordConfirmation { get; set; }
    
    [Required]
    public required string Name { get; set; }
}

public class LoginRequest
{
    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
}