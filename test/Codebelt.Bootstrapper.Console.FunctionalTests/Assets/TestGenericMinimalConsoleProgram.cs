using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class TestGenericMinimalConsoleProgram : MinimalConsoleProgram<TestMinimalConsoleProgram>
    {
        public static HostApplicationBuilder CreateHostBuilderAccessor(string[] args) => CreateHostBuilder(args);

        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
