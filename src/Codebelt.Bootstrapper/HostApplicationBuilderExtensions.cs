using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting.Internal;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Extension methods for the <see cref="IHostApplicationBuilder"/>.
    /// </summary>
    public static class HostApplicationBuilderExtensions
    {
        /// <summary>
        /// Listens for Ctrl+C or SIGTERM and calls <see cref="IHostApplicationLifetime.StopApplication"/> to start the shutdown process.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostApplicationBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostApplicationBuilder"/> for chaining.</returns>
        /// <remarks>Complements the default implementation of <see cref="ConsoleLifetime"/>.</remarks>
        public static IHostApplicationBuilder UseBootstrapperLifetime(this IHostApplicationBuilder hostBuilder)
        {
            hostBuilder.Services.Replace(ServiceDescriptor.Singleton<IHostLifetime, BootstrapperLifetime>());
            hostBuilder.Services.AddSingleton<IHostLifetimeEvents>(provider => provider.GetRequiredService<IHostLifetime>() as BootstrapperLifetime);
            return hostBuilder;
        }
    }
}
