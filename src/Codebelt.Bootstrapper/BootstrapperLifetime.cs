using System;
using System.Threading;
using System.Threading.Tasks;
using Cuemon;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Listens for Ctrl+C or SIGTERM and initiates shutdown.
    /// </summary>
    /// <seealso cref="Disposable" />
    /// <seealso cref="IHostLifetime" />
    public class BootstrapperLifetime : Disposable, IHostLifetime, IHostLifetimeEvents
    {
        private readonly ConsoleLifetime _hostLifetime;
        private readonly IHostApplicationLifetime _applicationLifetime;

        /// <summary>
        /// Triggered when the application host has fully started.
        /// </summary>
        public Action OnApplicationStartedCallback { get; set; }

        /// <summary>
        /// Triggered when the application host is starting a graceful shutdown.
        /// </summary>
        public Action OnApplicationStoppingCallback { get; set; }

        /// <summary>
        /// Triggered when the application host has completed a graceful shutdown./// </summary>
        public Action OnApplicationStoppedCallback { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperLifetime"/> class.
        /// </summary>
        /// <param name="options">The dependency injected <see cref="IOptions{ConsoleLifetimeOptions}"/>.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment"/>.</param>
        /// <param name="applicationLifetime">The dependency injected <see cref="IHostApplicationLifetime"/>.</param>
        /// <param name="hostOptions">The dependency injected <see cref="IOptions{HostOptions}"/>.</param>
        /// <param name="loggerFactory">The dependency injected <see cref="ILoggerFactory"/>.</param>
        public BootstrapperLifetime(IOptions<ConsoleLifetimeOptions> options, IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, IOptions<HostOptions> hostOptions, ILoggerFactory loggerFactory)
        {
            _hostLifetime = new ConsoleLifetime(options, environment, applicationLifetime, hostOptions, loggerFactory);
            _applicationLifetime = applicationLifetime;
        }

        /// <summary>
        /// Called from <see cref="IHost.StopAsync(CancellationToken)"/> to indicate that the host is stopping and it's time to shut down.
        /// </summary>
        /// <param name="cancellationToken">Used to indicate when stop should no longer be graceful.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _hostLifetime.StopAsync(cancellationToken);
        }

        /// <summary>
        /// Called at the start of <see cref="IHost.StartAsync(CancellationToken)"/> which will wait until it's complete before
        /// continuing. This can be used to delay startup until signaled by an external event.
        /// </summary>
        /// <param name="cancellationToken">Used to abort program start.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
            _applicationLifetime.ApplicationStopped.Register(OnApplicationStopped);
            _applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            return _hostLifetime.WaitForStartAsync(cancellationToken);
        }

        private void OnApplicationStarted()
        {
            OnApplicationStartedCallback?.Invoke();
        }

        private void OnApplicationStopped()
        {
            OnApplicationStoppedCallback?.Invoke();
        }

        private void OnApplicationStopping()
        {
            OnApplicationStoppingCallback?.Invoke();
        }

        /// <summary>
        /// Called when this object is being disposed by either <see cref="Disposable.Dispose" /> or <see cref="Disposable.Dispose(System.Boolean)" /> having <c>disposing</c> set to <c>true</c> and <see cref="Disposable.Disposed" /> is <c>false</c>.
        /// </summary>
        protected override void OnDisposeManagedResources()
        {
            _hostLifetime.Dispose();
        }
    }
}
