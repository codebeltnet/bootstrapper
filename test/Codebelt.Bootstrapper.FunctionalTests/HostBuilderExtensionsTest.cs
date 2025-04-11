using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Bootstrapper
{
    public class HostBuilderExtensionsTest : Test
    {
        public HostBuilderExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void UseBootstrapperLifetime_ShouldRegisterBootstrapperLifetime()
        {
            using var test = HostTestFactory.Create(services =>
            {
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            });

            var bootstrapperLifetime = test.Host.Services.GetService<IHostLifetime>();

            Assert.NotNull(bootstrapperLifetime);
            Assert.IsType<BootstrapperLifetime>(bootstrapperLifetime);
        }

        [Fact]
        public void UseBootstrapperStartup_ShouldRegisterStartupFactory()
        {
            using var test = HostTestFactory.Create(services =>
            {
            }, hb =>
            {
                hb.UseBootstrapperStartup<TestStartup>();
            });

            var startupFactory = test.Host.Services.GetService<IStartupFactory<TestStartup>>();

            Assert.NotNull(startupFactory);
            Assert.IsType<StartupFactory<TestStartup>>(startupFactory);
        }
    }
}
