using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Assets
{
    public class TestBackgroundService : BackgroundService
    {
        private readonly ILogger<TestBackgroundService> _logger;
        private readonly IHostLifetimeEvents _events;

        public TestBackgroundService(
            ILogger<TestBackgroundService> logger,
            IHostLifetimeEvents events)
        {
            _logger = logger;
            _events = events;
        }

        public TimeSpan Elapsed { get; private set; } = TimeSpan.Zero;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sw = Stopwatch.StartNew();
            _events.OnApplicationStartedCallback += () =>
            {
                sw.Stop();
                Elapsed = sw.Elapsed;
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow.ToString("O"));
                await Task.Delay(TimeSpan.FromMilliseconds(50), stoppingToken);
            }
        }
    }
}
