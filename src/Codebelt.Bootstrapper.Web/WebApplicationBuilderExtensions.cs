using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace Codebelt.Bootstrapper.Web
{
    /// <summary>
    /// Extension methods for the <see cref="WebApplicationBuilder"/>.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Listens for Ctrl+C or SIGTERM and calls <see cref="IHostApplicationLifetime.StopApplication"/> to start the shutdown process.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="HostApplicationBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="HostApplicationBuilder"/> for chaining.</returns>
        /// <remarks>Complements the default implementation of <see cref="ConsoleLifetime"/>.</remarks>
        public static WebApplicationBuilder UseBootstrapperLifetime(this WebApplicationBuilder hostBuilder)
        {
            hostBuilder.Services.Replace(ServiceDescriptor.Singleton<IHostLifetime, BootstrapperLifetime>());
            return hostBuilder;
        }
    }
}
