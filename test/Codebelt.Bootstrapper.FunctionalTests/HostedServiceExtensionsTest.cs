using System;
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
            var timeToWait = TimeSpan.FromMilliseconds(50);

            using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
                services.AddHostedService<TestBackgroundService>();
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            }, new TestHostFixture());

            var lifetimeEvents = test.Host.Services.GetRequiredService<IHostLifetimeEvents>();

            // Introduce the artificial delay *inside* the announcement pipeline,
            // but don't compete with the service's own handler.
            lifetimeEvents.OnApplicationStartedCallback += () =>
            {
                Thread.Sleep(timeToWait);
            };

            var bgs = (TestBackgroundService)test.Host.Services.GetRequiredService<IHostedService>();

            await test.Host.StartAsync().ConfigureAwait(false);

            // Now wait for the background service to finish waiting for the announcement:
            var elapsed = await bgs.Completed.Task
                .WaitAsync(TimeSpan.FromSeconds(5))
                .ConfigureAwait(false);

            TestOutput.WriteLine($"{elapsed} >= {timeToWait}");

            Assert.True(elapsed >= timeToWait, $"{elapsed} >= {timeToWait}");
        }
    }
}
