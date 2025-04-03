using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class BootstrapperLifetimeTest : Test
    {
        public BootstrapperLifetimeTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void BootstrapperLifetime_ShouldBeRegistered()
        {

            using var test = GenericHostTestFactory.Create(services =>
            {

            }, hb =>
            {
                hb.UseBootstrapperLifetime();
                hb.UseBootstrapperStartup<TestStartup>();
            }, new TestHostFixture());

            var bootstrapperLifetime = test.ServiceProvider.GetService<IHostLifetime>();
            Assert.NotNull(bootstrapperLifetime);
            Assert.IsType<BootstrapperLifetime>(bootstrapperLifetime);
        }

        [Fact]
        public void BootstrapperLifetime_ShouldCallOnApplicationStarted()
        {
            var started = false;
            BootstrapperLifetime.OnApplicationStartedCallback = () => { started = true; };

            using var test = GenericHostTestFactory.Create(services =>
            {
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
        public void BootstrapperLifetime_ShouldCallOnApplicationStoppingCallback_And_OnApplicationStoppedCallback()
        {
            var stopped = false;
            var stopping = false;

            BootstrapperLifetime.OnApplicationStoppingCallback = () => { stopping = true; };
            BootstrapperLifetime.OnApplicationStoppedCallback = () => { stopped = true; };

            using var test = GenericHostTestFactory.Create(services =>
            {
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



        //public override void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddXunitTestLoggingOutputHelperAccessor();
        //    services.AddXunitTestLogging(TestOutput);
        //}

        //protected override void ConfigureHost(IHostBuilder hb)
        //{
        //    BootstrapperLifetime.OnApplicationStartedCallback = () => { _started = true; };
        //    BootstrapperLifetime.OnApplicationStoppingCallback = () => { _stopping = true; };
        //    BootstrapperLifetime.OnApplicationStoppedCallback = () => { _stopped = true; };
        //    hb.UseBootstrapperLifetime();
        //    hb.UseBootstrapperStartup<TestStartup>();
        //}
    }
}
