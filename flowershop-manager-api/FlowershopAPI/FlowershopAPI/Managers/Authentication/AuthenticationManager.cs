using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlowerShopAPI.Common.Results;
using FlowerShopAPI.Contracts.Authentication;
using FlowerShopAPI.Managers.Authentication.Contract;
using FlowerShopAPI.Models;
using FlowerShopAPI.Common.Services.EmailSender.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FlowerShopAPI.Managers.Authentication;

public class AuthenticationManager(UserManager<User> userManager, IConfiguration configuration, IEmailSender emailSender) : IAuthenticationManager
{
    
    public async Task<OperationResult<RegisterAccountResponse>> Register(RegisterAccountRequest request)
    {
        if (request.Password != request.PasswordConfirmation)
        {
            return OperationResult<RegisterAccountResponse>.Failed(["Passwords don't match!"]);
        }
        
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return OperationResult<RegisterAccountResponse>.Failed(result.Errors.Select(e => e.Description).ToList());
        }

        await userManager.AddToRoleAsync(user, "User");
        
        RegisterAccountResponse response = new RegisterAccountResponse()
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            Name = user.Name,
        };

        return OperationResult<RegisterAccountResponse>.Success(response);
    }

    public async Task<OperationResult<LoginResponse>> Login(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return OperationResult<LoginResponse>.Failed(["Invalid credentials"]);
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
            Name = user.Name,
        };
        return OperationResult<LoginResponse>.Success(result);
    }

    public async Task SendForgotPasswordEmail(ForgotPasswordRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Email);
        
        if (user == null)
        {
            return;
        }
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"http://localhost:5173/reset-password?email={Uri.EscapeDataString(request.Email)}&token={Uri.EscapeDataString(token)}";
        
        await emailSender.SendEmailAsync(
            request.Email,
            "Reseteaza parola contului tau Flower Shop Manager",
            $"Am primit o solicitare de resetare a parolei contului tau de pe Flower Shop Manager.\n \n" +
            $"Daca nu tu ai facut aceasta solicitare, poti ignora acest mesaj.\n \n" +
            $"In schimb, pentru a continua procesul de resetare al parolei fa click pe acest link: {resetLink}"
        );
    }
}