using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// The base entry point of an application responsible for registering its <see cref="ConsoleStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="ProgramRoot{TStartup}" />
    public abstract class ConsoleProgram<TStartup> : ProgramRoot<TStartup> where TStartup : ConsoleStartup
    {
        static ConsoleProgram()
        {
            CreateHostBuilderCallback = args => Host.CreateDefaultBuilder(args)
                .UseBootstrapperLifetime()
                .UseBootstrapperStartup()
                .UseConsoleStartup<TStartup>();
        }

        /// <summary>
        /// Creates an <see cref="IHostBuilder"/> used to set up the host.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        protected new static IHostBuilder CreateHostBuilder(string[] args)
        {
            return ProgramRoot.CreateHostBuilder(args);
        }
    }
}
