using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class TestMinimalConsoleProgram : MinimalConsoleProgram
    {
        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TestMinimalConsoleProgram>>();
            logger.LogTrace($"Inside {nameof(RunAsync)} of {nameof(TestMinimalConsoleProgram)}.");
            return Task.CompletedTask;
        }
    }
}
