using Codebelt.Bootstrapper.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebApp
{
    /// <summary>
    /// Provides the base class of a conventional based <c>Startup</c> class for web applications.
    /// </summary>
    /// <seealso cref="WebStartup" />
    public abstract class WebAppStartup : WebStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAppStartup"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="T:Microsoft.Extensions.Hosting.IHostEnvironment" />.</param>
        protected WebAppStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
