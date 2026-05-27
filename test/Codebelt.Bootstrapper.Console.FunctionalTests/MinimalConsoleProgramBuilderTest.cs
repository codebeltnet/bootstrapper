using System.Linq;
using Codebelt.Bootstrapper.Console.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Console
{
    public class MinimalConsoleProgramBuilderTest : Test
    {
        public MinimalConsoleProgramBuilderTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateHostBuilder_ShouldRegisterMinimalConsoleServices()
        {
            using var host = TestMinimalConsoleProgram.CreateHostBuilderAccessor([]).Build();

            var lifetime = host.Services.GetRequiredService<IHostLifetime>();
            var programFactory = host.Services.GetRequiredService<IProgramFactory>();
            var hostedServices = host.Services.GetServices<IHostedService>().ToList();

            Assert.IsType<global::Codebelt.Bootstrapper.BootstrapperLifetime>(lifetime);
            Assert.NotNull(programFactory);
            Assert.Contains(hostedServices, hostedService => hostedService is MinimalConsoleHostedService);
        }

        [Fact]
        public void CreateHostBuilderOfTProgram_ShouldRegisterMinimalConsoleServices()
        {
            using var host = TestGenericMinimalConsoleProgram.CreateHostBuilderAccessor([]).Build();

            var programFactory = host.Services.GetRequiredService<IProgramFactory>();
            var hostedServices = host.Services.GetServices<IHostedService>().ToList();

            Assert.NotNull(programFactory);
            Assert.Contains(hostedServices, hostedService => hostedService is MinimalConsoleHostedService);
        }
    }
}
