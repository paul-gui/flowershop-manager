using System.ComponentModel.DataAnnotations;

namespace FlowerShopAPI.Contracts.Authentication;

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

public class ForgotPasswordRequest
{
    [Required]
    public string Email { get; set; }
}