using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await SeedData.Seed(context);
            }
            catch (Exception exception)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "An error occured during migration");
            }
            
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}