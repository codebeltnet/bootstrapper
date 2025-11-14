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
            var timeToWait = TimeSpan.FromMilliseconds(500);

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

            lifetimeEvents.OnApplicationStartedCallback += () =>
            {
                Thread.Sleep(timeToWait);
            };

            var bgs = (TestBackgroundService)test.Host.Services.GetRequiredService<IHostedService>();

            await test.Host.StartAsync().ConfigureAwait(false);
            
            TestOutput.WriteLine($"{bgs.Elapsed} >= {timeToWait}");

            Assert.True(bgs.Elapsed >= timeToWait, $"{bgs.Elapsed} >= {timeToWait}");
        }
    }
}
