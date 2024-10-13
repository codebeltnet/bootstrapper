using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// Provides a console application that is managed by its host.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="IHostedService" />
    public class ConsoleHostedService<TStartup> : IHostedService where TStartup : ConsoleStartup
    {
        private readonly IStartupFactory<TStartup> _factory;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IServiceProvider _provider;
        private ILogger<TStartup> _logger;
        private bool _ranToCompletion;
        private Task _runAsyncTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHostedService{TStartup}"/> class.
        /// </summary>
        /// <param name="factory">The dependency injected <see cref="IStartupFactory{TStartup}"/>.</param>
        /// <param name="applicationLifetime">The dependency injected <see cref="IHostApplicationLifetime"/>.</param>
        /// <param name="provider">The dependency injected <see cref="IServiceProvider"/>.</param>
        public ConsoleHostedService(IStartupFactory<TStartup> factory, IHostApplicationLifetime applicationLifetime, IServiceProvider provider)
        {
            _factory = factory;
            _applicationLifetime = applicationLifetime;
            _provider = provider;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var startup = _factory.Instance;

            _logger = _provider.GetRequiredService<ILogger<TStartup>>();

            _runAsyncTask = Task.Run(async () =>
            {
                try
                {
                    if (startup != null)
                    {
                        startup.ConfigureConsole(_provider);

                        await this.WaitForApplicationStartedAnnouncementAsync(cancellationToken).ConfigureAwait(false);

                        _logger.LogInformation("RunAsync started.");
                        await startup.RunAsync(_provider, cancellationToken).ConfigureAwait(false);
                        _ranToCompletion = true;
                    }
                    else
                    {
                        _logger.LogWarning("Unable to activate an instance of {TypeFullName}.", typeof(TStartup).FullName);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, "Fatal error occurred while activating {TypeFullName}.", typeof(TStartup).FullName);
                }
            }, cancellationToken);

            StartWaitForCompletionOfRunAsync().ConfigureAwait(false);

            return Task.CompletedTask;
        }

        private async Task StartWaitForCompletionOfRunAsync()
        {
            await _runAsyncTask.ConfigureAwait(false);
            _applicationLifetime.StopApplication();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (!_ranToCompletion)
            {
                _logger?.LogInformation("RunAsync ended prematurely.");
            }
            else
            {
                _logger?.LogInformation("RunAsync completed successfully.");
            }

            return Task.CompletedTask;
        }
    }
}
