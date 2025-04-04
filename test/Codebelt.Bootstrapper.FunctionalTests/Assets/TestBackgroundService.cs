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

        public TestBackgroundService(ILogger<TestBackgroundService> logger)
        {
            _logger = logger;
        }

        public TimeSpan Elapsed { get; private set; } = TimeSpan.Zero;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sw = Stopwatch.StartNew();
            await this.WaitForApplicationStartedAnnouncementAsync(stoppingToken).ConfigureAwait(false);
            sw.Stop();
            Elapsed = sw.Elapsed;
            _logger.LogInformation("TestBackgroundService started after {Elapsed}", Elapsed);
        }
    }
}
