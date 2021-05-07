using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Common
{
    /// <summary>
    /// Provides a console application that is managed by its host.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="IHostedService" />
    public class ConsoleHostedService<TStartup> : IHostedService where TStartup : ConsoleStartup
    {
        private readonly IStartupFactory _factory;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<TStartup> _logger;
        private bool _ranToCompletion = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHostedService{TStartup}"/> class.
        /// </summary>
        /// <param name="factory">The dependency injected <see cref="IStartupFactory"/>.</param>
        /// <param name="applicationLifetime">The dependency injected <see cref="IHostApplicationLifetime"/>.</param>
        /// <param name="logger">The dependency injected <see cref="ILogger{TCategoryName}"/>.</param>
        public ConsoleHostedService(IStartupFactory factory, IHostApplicationLifetime applicationLifetime, ILogger<TStartup> logger)
        {
            _factory = factory;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                try
                {
                    var startup = _factory.CreateInstance<TStartup>(out var services);
                    if (startup != null)
                    {
                        startup.Run(services.BuildServiceProvider(), _logger);

                        _logger.LogInformation("Run completed successfully.");
                        _ranToCompletion = true;
                    }
                    else
                    {
                        _logger.LogWarning($"Unable to activate an instance of {typeof(TStartup).FullName}.");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, $"Fatal error occurred while activating {typeof(TStartup).FullName}.");
                }
                finally
                {
                    _applicationLifetime.StopApplication();
                }

            }, cancellationToken);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (!_ranToCompletion) { _logger.LogInformation("Run ended prematurely."); }
            return Task.CompletedTask;
        }
    }
}
