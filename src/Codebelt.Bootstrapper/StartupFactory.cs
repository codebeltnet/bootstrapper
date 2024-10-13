using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// The default implementation of <see cref="IStartupFactory{TStartup}"/>.
    /// </summary>
    /// <seealso cref="IStartupFactory{TStartup}" />
    public class StartupFactory<TStartup> : IStartupFactory<TStartup> where TStartup : StartupRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupFactory{TStartup}"/> class.
        /// </summary>
        /// <param name="services">The dependency injected <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration"/>.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment"/>.</param>
        public StartupFactory(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var startup = (TStartup)Activator.CreateInstance(typeof(TStartup), configuration, environment);
            startup?.ConfigureServices(services);
            Instance = startup;

        }

        /// <summary>
        /// Provides access to an instance of the specified <typeparamref name="TStartup" />.
        /// </summary>
        /// <value>A reference to the object of type <see cref="StartupRoot"/>.</value>
        public TStartup Instance { get; }
    }
}
