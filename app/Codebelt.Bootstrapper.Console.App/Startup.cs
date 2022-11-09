using System;
using System.Threading;
using Cuemon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console.App
{
    public class Startup : ConsoleStartup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Run(IServiceProvider serviceProvider, ILogger logger)
        {
            BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogInformation("Started");
            BootstrapperLifetime.OnApplicationStoppingCallback = () =>
            {
                logger.LogWarning("Stopping and cleaning ..");
                Thread.Sleep(TimeSpan.FromSeconds(1)); // simulate graceful shutdown
                logger.LogWarning(".. done!");
            };
            BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");

            for (int dots = 0; dots <= 5; ++dots)
            {
                System.Console.Write($"\rFire and forget {Generate.FixedString('.', dots)}");
                Thread.Sleep(500);
            }

            System.Console.WriteLine("\nDone and done!");
        }
    }
}
