using Codebelt.Bootstrapper.Worker.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Worker
{
    public class WorkerProgramTest : Test
    {
        public WorkerProgramTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateHostBuilder_ShouldRegisterBootstrapperLifetimeAndStartup()
        {
            using var host = TestWorkerProgram.CreateHostBuilderAccessor([]).Build();

            var lifetime = host.Services.GetRequiredService<IHostLifetime>();
            var startupFactory = host.Services.GetRequiredService<global::Codebelt.Bootstrapper.IStartupFactory<TestStartup>>();

            Assert.IsType<global::Codebelt.Bootstrapper.BootstrapperLifetime>(lifetime);
            Assert.IsType<global::Codebelt.Bootstrapper.StartupFactory<TestStartup>>(startupFactory);
        }
    }
}
