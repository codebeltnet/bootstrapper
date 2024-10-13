using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
        /// <param name="configuration">The dependency injected <see cref="IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="IHostEnvironment" />.</param>
        protected WebStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        /// <summary>
        /// Configures the application pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> for the application to configure.</param>
        public abstract void ConfigurePipeline(IApplicationBuilder app);

        /// <summary>
        /// Delegates the application pipeline configuration to <see cref="ConfigurePipeline"/>.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> for the application to configure.</param>
        /// <remarks>This method is necessary as we rely on built-in convention based bootstrapping (<see cref="WebHostBuilderExtensions.UseStartup{TStartup}(IWebHostBuilder)"/>>).</remarks>
        public void Configure(IApplicationBuilder app) => ConfigurePipeline(app);
    }
}
