using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker.Assets
{
    internal class TestMinimalWorkerProgram : MinimalWorkerProgram
    {
        public static HostApplicationBuilder CreateHostBuilderAccessor(string[] args) => CreateHostBuilder(args);
    }
}
