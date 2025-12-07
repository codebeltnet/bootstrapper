using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console.Assets
{
    /// <summary>
    /// Test minimal console program that simulates an exception scenario during RunAsync execution.
    /// </summary>
    public class TestMinimalConsoleProgramWithException : MinimalConsoleProgram
    {
        /// <summary>
        /// Executes the console application logic and throws an exception to simulate an error scenario.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown intentionally to simulate an error during execution.</exception>
        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TestMinimalConsoleProgramWithException>>();
            logger.LogTrace($"Inside {nameof(RunAsync)} of {nameof(TestMinimalConsoleProgramWithException)}.");

            // Throw exception to simulate fatal error during execution
            throw new InvalidOperationException("Simulated exception in RunAsync for testing purposes.");
        }
    }
}
