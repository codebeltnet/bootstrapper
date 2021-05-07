using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Common
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
        /// <param name="configuration">The dependency injected <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="T:Microsoft.Extensions.Hosting.IHostEnvironment" />.</param>
        protected ConsoleStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        /// <summary>
        /// A convenient method for executing code.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        public abstract void Run(IServiceProvider serviceProvider, ILogger logger);
    }
}
