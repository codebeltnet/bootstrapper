using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Codebelt.Bootstrapper.Worker.Assets
{
    public class FakeHostedService : BackgroundService
    {
        private readonly TaskCompletionSource _tsc = new();
        private readonly ILogger<FakeHostedService> _logger;
        private readonly IHostLifetimeEvents _events;
        private bool _gracefulShutdown;

        public FakeHostedService(ILogger<FakeHostedService> logger, IHostLifetimeEvents events)
        {
            _logger = logger;

            events.OnApplicationStartedCallback = () =>
            {
                logger.LogInformation("Started");
                _tsc.SetResult();
            };
            events.OnApplicationStoppingCallback = () =>
            {
                _gracefulShutdown = true;
                logger.LogWarning("Stopping and cleaning ..");
                Thread.Sleep(TimeSpan.FromMilliseconds(125)); // simulate graceful shutdown
                logger.LogWarning(".. done!");
            };
            events.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");
            
            _events = events;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _tsc.Task.ConfigureAwait(false);
            var i = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_gracefulShutdown) { return; }
                _logger.LogInformation("Worker running in iterations: {iteration}", i);
                i++;
                await Task.Delay(TimeSpan.FromMilliseconds(1000), stoppingToken);
            }
        }
    }
}
