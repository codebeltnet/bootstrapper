using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker
{
    /// <summary>
    /// Provides the base class of a conventional based <c>Startup</c> class for a worker service.
    /// </summary>
    /// <seealso cref="StartupRoot" />
    public abstract class WorkerStartup : StartupRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerStartup"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment" />.</param>
        protected WorkerStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
