using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker.Assets
{
    internal class TestWorkerProgram : WorkerProgram<TestStartup>
    {
        public static IHostBuilder CreateHostBuilderAccessor(string[] args) => CreateHostBuilder(args);
    }
}
