namespace FlowershopAPI.Contracts.Authentication;

public class RegisterAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; }
}