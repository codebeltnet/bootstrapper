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
    /// Test console startup class that simulates a premature end scenario by stopping the application before RunAsync completes.
    /// </summary>
    public class TestConsoleStartupWithPrematureEnd : ConsoleStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestConsoleStartupWithPrematureEnd"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment" />.</param>
        public TestConsoleStartupWithPrematureEnd(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
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
        /// Executes the console application logic with a premature end by requesting application stop.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public override async Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TestConsoleStartupWithPrematureEnd>>();
            var lifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();

            logger.LogTrace($"Inside {nameof(RunAsync)} of {nameof(TestConsoleStartupWithPrematureEnd)}.");

            // Simulate premature end by stopping the application immediately
            lifetime.StopApplication();

            // This delay will never complete because the application stops
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
