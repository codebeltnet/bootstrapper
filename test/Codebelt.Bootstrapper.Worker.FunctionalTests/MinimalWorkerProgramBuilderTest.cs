using Codebelt.Bootstrapper.Worker.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Worker
{
    public class MinimalWorkerProgramBuilderTest : Test
    {
        public MinimalWorkerProgramBuilderTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateHostBuilder_ShouldRegisterBootstrapperLifetime()
        {
            using var host = TestMinimalWorkerProgram.CreateHostBuilderAccessor([]).Build();

            var lifetime = host.Services.GetRequiredService<IHostLifetime>();

            Assert.IsType<global::Codebelt.Bootstrapper.BootstrapperLifetime>(lifetime);
        }
    }
}
