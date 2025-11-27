using FlowershopAPI.Contracts.Authentication;

namespace FlowershopAPI.Managers.Authentication.Contract;

public interface IAuthenticationManager
{
    public Task<RegisterAccountResponse?> Register(RegisterAccountRequest request);
    public Task<LoginResponse?> Login(LoginRequest request);
}