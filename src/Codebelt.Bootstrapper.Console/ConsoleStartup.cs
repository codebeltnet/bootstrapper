using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// Provides the base class of a conventional based <c>Startup</c> class for a console application.
    /// </summary>
    /// <seealso cref="StartupRoot" />
    public abstract class ConsoleStartup : StartupRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleStartup"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment" />.</param>
        protected ConsoleStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        /// <summary>
        /// Provides access to previously registered services.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        public abstract void UseServices(IServiceProvider serviceProvider);

        /// <summary>
        /// A convenient method for executing fire-and-forget code.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        public abstract Task RunAsync(CancellationToken cancellationToken);
    }
}
