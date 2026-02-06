using FlowerShopAPI.Common.Results;
using FlowerShopAPI.Contracts.Authentication;
using ForgotPasswordRequest = FlowerShopAPI.Contracts.Authentication.ForgotPasswordRequest;
using LoginRequest = FlowerShopAPI.Contracts.Authentication.LoginRequest;

namespace FlowerShopAPI.Managers.Authentication.Contract;

public interface IAuthenticationManager
{
    public Task<OperationResult<RegisterAccountResponse>> Register(RegisterAccountRequest request);
    public Task<OperationResult<LoginResponse>> Login(LoginRequest request);
    public Task SendForgotPasswordEmail(ForgotPasswordRequest request);
    public Task<OperationResult<string>> ResetPassword(ResetPasswordRequest request);
}