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
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IHostLifetime _hostLifetime;
        private IHostLifetimeEvents _events;

        public TestBackgroundService(ILogger<TestBackgroundService> logger, IHostApplicationLifetime applicationLifetime, IHostLifetimeEvents events)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;
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
                _logger.LogInformation("TestBackgroundService started after {Elapsed}", Elapsed);
            };
        }
    }
}
