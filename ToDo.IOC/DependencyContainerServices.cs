using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDo.Application.Contracts.Persistance;
using ToDo.Application.Contracts.Security;
using ToDo.Infrastructure.Persistance;
using ToDo.Infrastructure.Persistance.Repositories;
using ToDo.Infrastructure.Security;

namespace ToDo.IOC
{
    public static class DependencyContainerServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //JWT Setting and configuration
            var JWTSetting = configuration.GetSection("JwtSettings");
            var SecretKey = Encoding.UTF8.GetBytes(JWTSetting["SecretKey"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidIssuer = JWTSetting["Issuer"],
                          ValidAudience = JWTSetting["Audience"],
                          IssuerSigningKey = new SymmetricSecurityKey(SecretKey)
                      };

                      options.Events = new JwtBearerEvents
                      {
                          OnChallenge = context =>
                          {
                              Console.WriteLine("OnChallenge triggered");
                              context.Response.StatusCode = 401; // Unauthorized
                              context.Response.ContentType = "application/json";
                              return Task.CompletedTask;

                          }
                      };
                  });


            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
