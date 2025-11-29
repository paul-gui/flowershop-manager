using FlowershopAPI.Common.Results;
using FlowershopAPI.Contracts.Authentication;

namespace FlowershopAPI.Managers.Authentication.Contract;

public interface IAuthenticationManager
{
    public Task<OperationResult<RegisterAccountResponse>> Register(RegisterAccountRequest request);
    public Task<OperationResult<LoginResponse>> Login(LoginRequest request);
}