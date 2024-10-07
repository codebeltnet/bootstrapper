using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// The default implementation of <see cref="IStartupFactory"/>.
    /// </summary>
    /// <seealso cref="IStartupFactory" />
    public class StartupFactory : IStartupFactory
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupFactory"/> class.
        /// </summary>
        /// <param name="services">The dependency injected <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration"/>.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment"/>.</param>
        public StartupFactory(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            _services = services;
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// Creates an instance of the specified <typeparamref name="TStartup" />.
        /// </summary>
        /// <typeparam name="TStartup">The type to create.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>A reference to the newly created object of type <see cref="StartupRoot" />.</returns>
        public TStartup CreateInstance<TStartup>(out IServiceCollection services) where TStartup : StartupRoot
        {
            var startup = (TStartup)Activator.CreateInstance(typeof(TStartup), _configuration, _environment);
            startup?.ConfigureServices(_services);
            services = _services;
            return startup;
        }
    }
}
