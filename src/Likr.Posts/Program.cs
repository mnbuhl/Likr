using System;
using Likr.Posts.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Likr.Posts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();

            try
            {
                var seeder = new DatabaseSeeder(context);
                seeder.Seed();
            }
            catch (Exception e)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, "Error seeding database.");
            }

            scope.Dispose();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}