using System;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// The base entry point of an application responsible for registering its <see cref="StartupRoot"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="ProgramRoot" />
    public abstract class ProgramRoot<TStartup> : ProgramRoot where TStartup : StartupRoot
    {
    }

    /// <summary>
    /// The base entry point of an application responsible for registering its <see cref="StartupRoot"/> partner.
    /// </summary>
    public abstract class ProgramRoot
    {
        static ProgramRoot()
        {
            CreateHostBuilderCallback = args => Host.CreateDefaultBuilder(args).UseBootstrapperLifetime();
        }

        /// <summary>
        /// Creates an <see cref="IHostBuilder"/> used to set up the host.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        protected static IHostBuilder CreateHostBuilder(string[] args)
        {
            return CreateHostBuilderCallback?.Invoke(args);
        }

        /// <summary>
        /// Gets or sets the function delegate responsible for initializing an instance of <see cref="IHostBuilder"/>.
        /// </summary>
        /// <value>The function delegate responsible for initializing an instance of <see cref="IHostBuilder"/>.</value>
        protected static Func<string[], IHostBuilder> CreateHostBuilderCallback { get; set; }
    }
}
