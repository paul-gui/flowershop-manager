using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlowershopAPI.Contracts.Authentication;

namespace FlowershopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(UserManager<User> userManager, IConfiguration configuration)
        : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, "User");

            return Ok("User registered");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await userManager.FindByNameAsync(loginRequest.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }.Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}

