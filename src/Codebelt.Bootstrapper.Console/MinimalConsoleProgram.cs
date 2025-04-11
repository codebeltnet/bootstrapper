using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// The base entry point of an application optimized for console applications.
    /// </summary>
    public abstract class MinimalConsoleProgram : ProgramRoot
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
            hb.UseBootstrapperProgram(typeof(MinimalConsoleProgram));
            hb.UseMinimalConsoleProgram();
            return hb;
        }

        /// <summary>
        /// A convenient method for executing fire-and-forget code.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        public abstract Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }
}
