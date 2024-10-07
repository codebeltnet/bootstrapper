using System;
using System.Threading;
using System.Threading.Tasks;
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

        public override void UseServices(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

            BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogInformation("Started");
            BootstrapperLifetime.OnApplicationStoppingCallback = () =>
            {
                logger.LogWarning("Stopping and cleaning ..");
                Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate graceful shutdown
                logger.LogWarning(".. done!");
            };
            BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            for (int dots = 0; dots <= 5; ++dots)
            {
                if (cancellationToken.IsCancellationRequested) { return; }
                System.Console.Write($"\rFire and forget {Generate.FixedString('.', dots)}");
                await Task.Delay(500, cancellationToken).ConfigureAwait(false);
            }

            System.Console.WriteLine("\nDone and done!");
        }
    }
}
