using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
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
            BootstrapperLifetime.OnApplicationStartedCallback += () => { started = true; };

            using var test = GenericHostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
                hb.UseBootstrapperStartup<TestStartup>();
            }, new TestHostFixture());

            test.Host.Start();

            Assert.True(started);
        }

        [Fact]
        public void OnApplicationStoppingCallback_OnApplicationStoppedCallback_ShouldBeInvokedWhenStoppingHost()
        {
            var stopped = false;
            var stopping = false;

            BootstrapperLifetime.OnApplicationStoppingCallback += () => { stopping = true; };
            BootstrapperLifetime.OnApplicationStoppedCallback += () => { stopped = true; };

            using var test = GenericHostTestFactory.Create(services =>
            {
                services.AddXunitTestLoggingOutputHelperAccessor();
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
                hb.UseBootstrapperStartup<TestStartup>();
            }, new TestHostFixture());

            test.Host.Start();
            test.Host.StopAsync().GetAwaiter().GetResult();

            Assert.True(stopping && stopped);
        }
    }
}
