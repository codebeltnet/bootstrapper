using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Cuemon;
using Cuemon.Threading;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
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
                services.AddXunitTestLogging(TestOutput);
                services.AddHostedService<TestBackgroundService>();
                services.Configure<HostOptions>(o => o.ShutdownTimeout = TimeSpan.FromSeconds(5));
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            }, new TestHostFixture());

            var lifetimeEvents = test.Host.Services.GetRequiredService<IHostLifetimeEvents>();

            lifetimeEvents.OnApplicationStartedCallback += () =>
            {
                Thread.Sleep(timeToWait);
            };

            var reset = false;
            lifetimeEvents.OnApplicationStoppingCallback += () =>
            {
                reset = true;
            };

            var bgs = (TestBackgroundService)test.Host.Services.GetRequiredService<IHostedService>();

            await test.Host.StartAsync().ConfigureAwait(false);

            await Awaiter.RunUntilSuccessfulOrTimeoutAsync(() => reset
                ? Task.FromResult<ConditionalValue>(new SuccessfulValue())
                : Task.FromResult<ConditionalValue>(new UnsuccessfulValue())).ConfigureAwait(false);

            TestOutput.WriteLine($"{bgs.Elapsed} >= {timeToWait}");

            Assert.True(bgs.Elapsed >= timeToWait, $"{bgs.Elapsed} >= {timeToWait}");
        }
    }
}
