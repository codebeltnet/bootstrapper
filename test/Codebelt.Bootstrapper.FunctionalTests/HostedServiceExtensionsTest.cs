using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

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
            var started = false;
            BootstrapperLifetime.OnApplicationStartedCallback = () =>
            {
                Thread.Sleep(timeToWait);
            };

            using var test = GenericHostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
                services.AddHostedService<TestBackgroundService>();
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            }, new TestHostFixture());

            var bgs = test.ServiceProvider.GetRequiredService<IHostedService>() as TestBackgroundService;

            await test.Host.StartAsync().ConfigureAwait(false);

            await Task.Delay(timeToWait).ConfigureAwait(false);

            Assert.True(bgs.Elapsed >= timeToWait, $"{bgs.Elapsed} >= {timeToWait}");
        }
    }
}
