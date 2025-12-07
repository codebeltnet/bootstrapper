using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console.Assets
{
    /// <summary>
    /// Test console startup class that simulates an exception scenario during RunAsync execution.
    /// </summary>
    public class TestConsoleStartupWithException : ConsoleStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestConsoleStartupWithException"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment" />.</param>
        public TestConsoleStartupWithException(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        /// <summary>
        /// Configures services for the console application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Executes the console application logic and throws an exception to simulate an error scenario.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown intentionally to simulate an error during execution.</exception>
        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TestConsoleStartupWithException>>();
            logger.LogTrace($"Inside {nameof(RunAsync)} of {nameof(TestConsoleStartupWithException)}.");

            // Throw exception to simulate fatal error during execution
            throw new InvalidOperationException("Simulated exception in RunAsync for testing purposes.");
        }
    }
}
