using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlowershopAPI.Contracts.Authentication;
using FlowershopAPI.Managers.Authentication.Contract;

namespace FlowershopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationManager authenticationManager)
        : ControllerBase
    {
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await authenticationManager.Login(loginRequest);

            if (!result.Succeeded)
            {
                return Unauthorized(result.Errors);
            }
            
            return Ok(result.Data);
        }
    }
}

