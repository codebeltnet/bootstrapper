using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

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

        /// <summary>
        /// Adds conventional environment defaults for local development.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostApplicationBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostApplicationBuilder"/> for chaining.</returns>
        /// <remarks>
        /// When the current environment is local development, this method attempts to resolve the application assembly from <see cref="IHostEnvironment.ApplicationName"/>
        /// and adds user secrets to <see cref="IConfigurationBuilder"/>. If the application assembly cannot be resolved, the operation is ignored.
        /// </remarks>
        public static IHostApplicationBuilder UseBootstrapperEnvironmentDefaults(this IHostApplicationBuilder hostBuilder)
        {
            if (!hostBuilder.Environment.IsLocalDevelopment()) { return hostBuilder; }
            if (hostBuilder.Environment.ApplicationName is not { Length: > 0 } applicationName) { return hostBuilder; }
            var reloadOnChange = hostBuilder.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);

            try
            {
                var appAssembly = Assembly.Load(new AssemblyName(applicationName));
                hostBuilder.Configuration.AddUserSecrets(appAssembly, optional: true, reloadOnChange: reloadOnChange);
            }
            catch (FileNotFoundException)
            {
                // The assembly cannot be found, so just skip it.
            }

            return hostBuilder;
        }
    }
}
