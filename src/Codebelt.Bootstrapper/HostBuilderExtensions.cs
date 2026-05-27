using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Reflection;

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
                services.AddSingleton<IHostLifetimeEvents>(provider => provider.GetRequiredService<IHostLifetime>() as BootstrapperLifetime);
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

        /// <summary>
        /// Adds conventional environment defaults for local development.
        /// </summary>
        /// <typeparam name="TStartup">The type of the startup class used to resolve user secrets.</typeparam>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        /// <remarks>
        /// When the current environment is local development, this method adds user secrets for <typeparamref name="TStartup"/> to the application configuration.
        /// </remarks>
        public static IHostBuilder UseBootstrapperEnvironmentDefaults<TStartup>(this IHostBuilder hostBuilder) where TStartup : StartupRoot
        {
            return hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                var environment = context.HostingEnvironment;
                if (!environment.IsLocalDevelopment()) { return; }
                var reloadOnChange = context.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);
                builder.AddUserSecrets<TStartup>(optional: true, reloadOnChange: reloadOnChange);
            });
        }
    }
}
