using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var builder = new NpgsqlConnectionStringBuilder()
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                Username = configuration["User ID"],
                Password = configuration["Password"]
            };
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(builder.ConnectionString));
            return services;
        }
    }
}