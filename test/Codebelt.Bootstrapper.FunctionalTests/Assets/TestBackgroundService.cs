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
        private readonly Stopwatch _stopwatch;

        public TestBackgroundService(ILogger<TestBackgroundService> logger, IHostLifetimeEvents events, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _events = events;
            _stopwatch = Stopwatch.StartNew();

            _events.OnApplicationStartedCallback += () =>
            {
                _logger.LogInformation("TestBackgroundService started after {Elapsed}", Elapsed);
                applicationLifetime.StopApplication();
            };
        }

        public TimeSpan Elapsed { get; private set; } = TimeSpan.Zero;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1), stoppingToken).ConfigureAwait(false);
                }
                finally
                {
                    Elapsed = _stopwatch.Elapsed;
                }
            }
        }
    }
}
