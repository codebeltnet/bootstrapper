using System;
using System.Threading;
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
        private bool _started = false;
        private bool _stopping = false;
        private bool _stopped = false;

        public BootstrapperLifetimeTest(ITestOutputHelper output) : base(output)
        {
            BootstrapperLifetime.OnApplicationStartedCallback = () => { _started = true; };
            BootstrapperLifetime.OnApplicationStoppingCallback = () => { _stopping = true; };
            BootstrapperLifetime.OnApplicationStoppedCallback = () => { _stopped = true; };
        }

        [Fact]
        public void OnApplicationStartedCallback_ShouldBeInvokedWhenStartingHost()
        {
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

            Assert.True(_started);
        }

        [Fact]
        public void OnApplicationStoppingCallback_OnApplicationStoppedCallback_ShouldBeInvokedWhenStoppingHost()
        {
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

            Assert.True(_stopping && _stopped);
        }
    }
}
