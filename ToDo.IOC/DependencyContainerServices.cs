using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();

            return services;
        }
    }
}
