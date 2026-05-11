using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using FlowerShopAPI.Common.Configuration;
using FlowerShopAPI.Common.Services.EmailSender;
using FlowerShopAPI.Common.Services.EmailSender.Contract;
using FlowerShopAPI.Data;
using FlowerShopAPI.Managers.Authentication;
using FlowerShopAPI.Managers.Authentication.Contract;
using FlowerShopAPI.Managers.Products;
using FlowerShopAPI.Managers.Products.Contract;
using FlowerShopAPI.Managers.Sales;
using FlowerShopAPI.Managers.Sales.Contract;
using FlowerShopAPI.Managers.Warehouses;
using FlowerShopAPI.Managers.Warehouses.Contract;
using FlowerShopAPI.Models;

namespace FlowerShopAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            //Add EF Core
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Add Identity
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Configure password requirements
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            // Change lifetime of tokens
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options => 
                options.TokenLifespan = TimeSpan.FromHours(2)); 

            //Configure CORS to allow requests from frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy =>
                    {
                        policy.WithOrigins($"{configuration["App:FrontendBaseUrl"]}")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials(); 
                    });
            });

            //Configure JWT
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = configuration["Jwt:Key"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    RoleClaimType = ClaimTypes.Role
                };
            });
            builder.Services.AddAuthorization();

            // Add rate limiter for login
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                
                options.AddPolicy("auth-policy", httpContext =>
                {
                    var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    
                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    });
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(Program));
            
            builder.Services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            builder.Services.AddTransient<IWarehousesManager, WarehousesManager>();
            builder.Services.AddTransient<IProductsManager, ProductsManager>();
            builder.Services.AddTransient<ISalesManager, SalesManager>();
            
            //Configure email service
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            var config = builder.Configuration.GetSection("EmailSettings");
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowVueApp");

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = services.GetRequiredService<ApplicationDbContext>();

                await DbSeeds.SeedRoles(roleManager);
                await DbSeeds.SeedDestinations(context);
            }

            app.Run();
        }
    }
}
