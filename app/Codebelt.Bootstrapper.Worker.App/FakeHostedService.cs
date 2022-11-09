using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Worker.App
{
    public class FakeHostedService : BackgroundService
    {
        private readonly ILogger<FakeHostedService> _logger;
        private bool _gracefulShutdown = false;

        public FakeHostedService(ILogger<FakeHostedService> logger)
        {
            _logger = logger;

            BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogInformation("Started");
            BootstrapperLifetime.OnApplicationStoppingCallback = () =>
            {
                _gracefulShutdown = true;
                logger.LogWarning("Stopping and cleaning ..");
                Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate graceful shutdown
                logger.LogWarning(".. done!");
            };
            BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_gracefulShutdown) { return; }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow.ToString("O"));
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
