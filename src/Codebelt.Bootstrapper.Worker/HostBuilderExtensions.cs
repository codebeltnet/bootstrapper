using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker
{
    /// <summary>
    /// Extension methods for the <see cref="IHostBuilder"/>.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Provides an implementation of a conventional based <see cref="IStartupFactory"/>.
        /// </summary>
        /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder UseWorkerStartup<TStartup>(this IHostBuilder hostBuilder) where TStartup : WorkerStartup
        {
            return hostBuilder.ConfigureServices(services =>
            {
                var provider = services.BuildServiceProvider();
                var factory = provider.GetService<IStartupFactory>();
                factory?.CreateInstance<TStartup>(out _);
            });
        }
    }
}
