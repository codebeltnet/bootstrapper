using System;
using System.Threading;
using System.Threading.Tasks;
using Cuemon;
using Cuemon.Messaging;
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
            services.AddSingleton<ICorrelationToken>(new CorrelationToken());
        }

        public override void ConfigureConsole(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
            BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogWarning("Console started.");
            BootstrapperLifetime.OnApplicationStoppingCallback = () =>
            {
                logger.LogWarning("Stopping and cleaning ..");
                Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate graceful shutdown
                logger.LogWarning(".. done!");
            };
            BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Console stopped.");
        }

        public async override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

            logger.LogInformation("Guid: {Guid}", serviceProvider.GetRequiredService<ICorrelationToken>());

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
