using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Provides the base class of a conventional based <c>Startup</c> class.
    /// </summary>
    public abstract class StartupRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupRoot"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="IConfiguration"/>.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment"/>.</param>
        protected StartupRoot(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// Gets the <see cref="IConfiguration"/> of this instance.
        /// </summary>
        /// <value>The <see cref="IConfiguration"/> of this instance.</value>
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the <see cref="IHostEnvironment"/> of this instance.
        /// </summary>
        /// <value>The <see cref="IHostEnvironment"/> of this instance.</value>
        protected IHostEnvironment Environment { get; }

        /// <summary>
        /// Register services into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        public abstract void ConfigureServices(IServiceCollection services);
    }
}
