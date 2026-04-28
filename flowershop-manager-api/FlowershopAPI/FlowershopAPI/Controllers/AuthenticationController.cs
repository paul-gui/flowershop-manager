using Microsoft.AspNetCore.Mvc;
using FlowerShopAPI.Contracts.Authentication;
using FlowerShopAPI.Managers.Authentication.Contract;
using Microsoft.AspNetCore.RateLimiting;

namespace FlowerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationManager authenticationManager)
        : ControllerBase
    {
        [EnableRateLimiting("auth-policy")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
        {
            var result = await authenticationManager.Register(request);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [EnableRateLimiting("auth-policy")]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await authenticationManager.Login(loginRequest);

            if (!result.Succeeded)
            {
                return Unauthorized(result.Errors);
            }
            
            return Ok(result.Data);
        }

        [EnableRateLimiting("auth-policy")]
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            await authenticationManager.SendForgotPasswordEmail(request);
            return Ok();
        }

        [EnableRateLimiting("auth-policy")]
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await authenticationManager.ResetPassword(request);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok(result.Data);
        }
    }
}

