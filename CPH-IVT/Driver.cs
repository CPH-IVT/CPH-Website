using CPH_IVT.Services.MongoDB.Init;
using CPH_IVT.Services.MongoDB.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace CPH_IVT
{
    /// <summary>
    /// 
    /// </summary>
    public class Driver
    {
        // Moved to the Async seeder to lower the number of params
        // private static readonly string CurrentPath = Environment.CurrentDirectory;
        // private static readonly string DataDirectory = Path.GetFullPath(Path.Combine(CurrentPath, "..", "..", "data", "indicators"));

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedData(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SeedData(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var initializer = services.GetRequiredService<InitializerAsync>();
                var censusRegionRepo = services.GetRequiredService<ICensusRegionRepository>();
                var censusDivisionRepo = services.GetRequiredService<ICensusDivisionRepository>();
                var stateRepo = services.GetRequiredService<IStateRepository>();
                var countyRepo = services.GetRequiredService<ICountyRepository>();
                var healthIndicatorRepo = services.GetRequiredService<IHealthIndicatorRepository>();

                // Database seeder
               // initializer.FuckingSendIt(healthIndicatorRepo, countyRepo, stateRepo, censusDivisionRepo, censusRegionRepo);
            }
            catch (Exception e)
            {
                var logger = services.GetRequiredService<ILogger<Driver>>();
                logger.LogError(e.Message);
            }
        }
    }
}
