using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper
{
    public class HostedServiceExtensionsTest : Test
    {
        public HostedServiceExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task WaitForApplicationStartedAnnouncementAsync_MustWaitForApplicationStarted()
        {
            // increase baseline to be CI-friendly
            var timeToWait = TimeSpan.FromMilliseconds(250);
            var started = false;

            using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
                services.AddHostedService<TestBackgroundService>();
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            }, new TestHostFixture());

            // Simulate synchronous work on application started, but keep the delay sizable for CI.
            // Using GetResult on Task.Delay keeps behaviour similar (blocking) while keeping the
            // timing easier to adjust than tiny Thread.Sleep values.
            test.Host.Services.GetRequiredService<IHostLifetimeEvents>().OnApplicationStartedCallback += () =>
            {
                Task.Delay(timeToWait).GetAwaiter().GetResult();
            };

            var bgs = test.Host.Services.GetRequiredService<IHostedService>() as TestBackgroundService;

            await test.Host.StartAsync().ConfigureAwait(false);

            // Rather than a single fixed Task.Delay then assert, poll until the condition is met
            // or timeout. This is more resilient to scheduling jitter on CI.
            await WaitUntilAsync(() => bgs.Elapsed >= timeToWait, TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(50))
                .ConfigureAwait(false);

                Assert.True(bgs.Elapsed >= timeToWait, $"{bgs.Elapsed} >= {timeToWait}");
        }

        private static async Task WaitUntilAsync(Func<bool> condition, TimeSpan timeout, TimeSpan? pollInterval = null)
        {
            var interval = pollInterval ?? TimeSpan.FromMilliseconds(50);
            var sw = Stopwatch.StartNew();
            while (sw.Elapsed < timeout)
            {
                if (condition()) return;
                await Task.Delay(interval).ConfigureAwait(false);
            }
            throw new TimeoutException($"Condition not met within {timeout} (last elapsed {sw.Elapsed}).");
        }
    }
}
