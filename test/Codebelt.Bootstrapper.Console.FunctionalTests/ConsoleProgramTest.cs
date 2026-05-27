using System.Linq;
using Codebelt.Bootstrapper.Console.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Console
{
    public class ConsoleProgramTest : Test
    {
        public ConsoleProgramTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateHostBuilder_ShouldRegisterBootstrapperServices()
        {
            using var host = TestConsoleProgram.CreateHostBuilderAccessor([]).Build();

            var lifetime = host.Services.GetRequiredService<IHostLifetime>();
            var startupFactory = host.Services.GetRequiredService<global::Codebelt.Bootstrapper.IStartupFactory<TestConsoleStartup>>();
            var hostedServices = host.Services.GetServices<IHostedService>().ToList();

            Assert.IsType<global::Codebelt.Bootstrapper.BootstrapperLifetime>(lifetime);
            Assert.IsType<global::Codebelt.Bootstrapper.StartupFactory<TestConsoleStartup>>(startupFactory);
            Assert.Contains(hostedServices, hostedService => hostedService is ConsoleHostedService<TestConsoleStartup>);
        }
    }
}
