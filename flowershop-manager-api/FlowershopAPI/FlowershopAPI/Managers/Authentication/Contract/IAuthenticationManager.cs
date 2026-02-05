using FlowerShopAPI.Common.Results;
using FlowerShopAPI.Contracts.Authentication;

namespace FlowerShopAPI.Managers.Authentication.Contract;

public interface IAuthenticationManager
{
    public Task<OperationResult<RegisterAccountResponse>> Register(RegisterAccountRequest request);
    public Task<OperationResult<LoginResponse>> Login(LoginRequest request);
}