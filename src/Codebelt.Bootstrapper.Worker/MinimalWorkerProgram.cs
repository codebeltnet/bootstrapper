using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker
{
    /// <summary>
    /// The base entry point of an application optimized for worker applications.
    /// </summary>
    public abstract class MinimalWorkerProgram : ProgramRoot
    {
        /// <summary>
        /// Creates an <see cref="HostApplicationBuilder"/> used to set up the host.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="HostApplicationBuilder"/>.</returns>
        protected static HostApplicationBuilder CreateHostBuilder(string[] args)
        {
            var hb = Host.CreateApplicationBuilder(args);
            hb.UseBootstrapperLifetime();
            return hb;
        }
    }
}
