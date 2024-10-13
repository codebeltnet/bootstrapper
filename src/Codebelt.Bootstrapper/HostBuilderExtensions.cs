using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Extension methods for the <see cref="IHostBuilder"/>.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Listens for Ctrl+C or SIGTERM and calls <see cref="IHostApplicationLifetime.StopApplication"/> to start the shutdown process.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        /// <remarks>Complements the default implementation of <see cref="ConsoleLifetime"/>.</remarks>
        public static IHostBuilder UseBootstrapperLifetime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.Replace(ServiceDescriptor.Singleton<IHostLifetime, BootstrapperLifetime>());
            });
        }

        /// <summary>
        /// Provides an implementation of a conventional based <see cref="IStartupFactory{TStartup}"/>.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder UseBootstrapperStartup<TStartup>(this IHostBuilder hostBuilder) where TStartup : StartupRoot
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IStartupFactory<TStartup>>(new StartupFactory<TStartup>(services, context.Configuration, context.HostingEnvironment));
            });
        }
    }
}
