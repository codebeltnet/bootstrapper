using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker
{
    /// <summary>
    /// The entry point of an application responsible for registering its <see cref="WorkerStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="ProgramRoot{TStartup}" />
    public class WorkerProgram<TStartup> : ProgramRoot<TStartup> where TStartup : WorkerStartup
    {
        /// <summary>
        /// Creates an <see cref="IHostBuilder"/> used to set up the host.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        protected static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseBootstrapperLifetime()
                .UseBootstrapperStartup<TStartup>();
        }
    }
}
