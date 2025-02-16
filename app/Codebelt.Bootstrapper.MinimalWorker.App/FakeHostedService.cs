﻿namespace Codebelt.Bootstrapper.MinimalWorker.App
{
    public class FakeHostedService : BackgroundService
    {
        private readonly ILogger<FakeHostedService> _logger;
        private bool _gracefulShutdown;

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
            await this.WaitForApplicationStartedAnnouncementAsync(stoppingToken).ConfigureAwait(false);
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_gracefulShutdown) { return; }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow.ToString("O"));
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
