namespace FlowershopAPI.Contracts.Authentication;

public class RegisterAccountRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirmation { get; set; }
    public required string Name { get; set; }
}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}