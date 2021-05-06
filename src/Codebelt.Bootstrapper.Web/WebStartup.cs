using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Web
{
    /// <summary>
    /// Provides the base class of a conventional based <c>Startup</c> class for web applications.
    /// </summary>
    /// <seealso cref="StartupRoot" />
    public abstract class WebStartup : StartupRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebStartup"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="T:Microsoft.Extensions.Hosting.IHostEnvironment" />.</param>
        protected WebStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }



        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> for the application to configure.</param>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        public abstract void Configure(IApplicationBuilder app, ILogger logger);
    }
}
