using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console.Assets
{
    /// <summary>
    /// Test minimal console program that simulates a premature end scenario by stopping the application before RunAsync completes.
    /// </summary>
    public class TestMinimalConsoleProgramWithPrematureEnd : MinimalConsoleProgram
    {
        /// <summary>
        /// Executes the console application logic with a premature end by requesting application stop.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public override async Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TestMinimalConsoleProgramWithPrematureEnd>>();
            var lifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();

            logger.LogTrace($"Inside {nameof(RunAsync)} of {nameof(TestMinimalConsoleProgramWithPrematureEnd)}.");

            // Simulate premature end by stopping the application immediately
            lifetime.StopApplication();

            // This delay will never complete because the application stops
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
