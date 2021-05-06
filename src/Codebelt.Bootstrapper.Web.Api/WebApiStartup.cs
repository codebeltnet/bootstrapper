using Codebelt.Bootstrapper.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebApi
{
    /// <summary>
    /// Provides the base class of a conventional based <c>Startup</c> class for web applications.
    /// </summary>
    /// <seealso cref="WebStartup" />
    public abstract class WebApiStartup : WebStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiStartup"/> class.
        /// </summary>
        /// <param name="configuration">The dependency injected <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.</param>
        /// <param name="environment">The dependency injected <see cref="T:Microsoft.Extensions.Hosting.IHostEnvironment" />.</param>
        protected WebApiStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
