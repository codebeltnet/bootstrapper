using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// Extension methods for the <see cref="IHostBuilder"/>.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Provides an implementation of <see cref="IHostedService"/> that has the role of a console application.
        /// </summary>
        /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder UseConsoleStartup<TStartup>(this IHostBuilder hostBuilder) where TStartup : ConsoleStartup
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddHostedService<ConsoleHostedService<TStartup>>();
            });
        }
    }
}
