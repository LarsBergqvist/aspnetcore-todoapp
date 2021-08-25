using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Services;
using Infrastructure.Services;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("TodosConnection"));
            });
            services.AddTransient<ITodoService, TodoService>();
            return services;
        }
    }
}
