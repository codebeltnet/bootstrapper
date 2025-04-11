using Codebelt.Bootstrapper.Console;
using Cuemon.Messaging;
using Cuemon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.MinimalConsole.App
{
    internal class Program : MinimalConsoleProgram
    {
        static Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            builder.Services.AddSingleton<ICorrelationToken>(new CorrelationToken());

            var host = builder.Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            var events = host.Services.GetRequiredService<IHostLifetimeEvents>();

            events.OnApplicationStartedCallback = () => logger.LogWarning("Console started.");
            events.OnApplicationStoppingCallback = () =>
            {
                logger.LogWarning("Stopping and cleaning ..");
                Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate graceful shutdown
                logger.LogWarning(".. done!");
            };
            events.OnApplicationStoppedCallback = () => logger.LogCritical("Console stopped.");

            return host.RunAsync();
        }

        public async override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

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
