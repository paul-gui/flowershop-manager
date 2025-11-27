using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlowershopAPI.Contracts.Authentication;
using FlowershopAPI.Managers.Authentication.Contract;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FlowershopAPI.Managers.Authentication;

public class AuthenticationManager(UserManager<User> userManager, IConfiguration configuration) : IAuthenticationManager
{
    
    public async Task<RegisterAccountResponse?> Register(RegisterAccountRequest request)
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
            return null;
        }

        await userManager.AddToRoleAsync(user, "User");
        
        RegisterAccountResponse response = new RegisterAccountResponse()
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            Name = user.Name,
        };

        return response;
    }

    public async Task<LoginResponse?> Login(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return null;
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

        var result = new LoginResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
        return result;
    }
}