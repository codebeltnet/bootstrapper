using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Bootstrapper
{
    public class BootstrapperLifetimeTest : Test
    {
        public BootstrapperLifetimeTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void OnApplicationStartedCallback_ShouldBeInvokedWhenStartingHost()
        {
            var started = false;
            using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
                hb.UseBootstrapperStartup<TestStartup>();
            }, new TestHostFixture());

            test.Host.Services.GetRequiredService<IHostLifetimeEvents>().OnApplicationStartedCallback = () => { started = true; };

            test.Host.Start();

            Assert.True(started);
        }

        [Fact]
        public void OnApplicationStoppingCallback_OnApplicationStoppedCallback_ShouldBeInvokedWhenStoppingHost()
        {
            var stopping = false;
            var stopped = false;
            using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
                hb.UseBootstrapperStartup<TestStartup>();
            }, new TestHostFixture());

            test.Host.Services.GetRequiredService<IHostLifetimeEvents>().OnApplicationStoppingCallback = () => { stopping = true; };
            test.Host.Services.GetRequiredService<IHostLifetimeEvents>().OnApplicationStoppedCallback = () => { stopped = true; };

            test.Host.Start();
            test.Host.StopAsync().GetAwaiter().GetResult();

            Assert.True(stopping && stopped);
        }
    }
}
