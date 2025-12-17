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
        protected static HostApplicationBuilder CreateHostBuilder(string[] args) => CreateHostBuilder(args, typeof(MinimalConsoleProgram));

        /// <summary>
        /// A convenient method for executing fire-and-forget code.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to retrieve services from.</param>
        /// <param name="cancellationToken">Indicates that the run has been aborted.</param>
        /// <returns>A <see cref="Task"/> that completes when the execution is finished.</returns>
        public abstract Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);

        internal static HostApplicationBuilder CreateHostBuilder(string[] args, Type programType)
        {
            var hb = Host.CreateApplicationBuilder(args);
            hb.UseBootstrapperLifetime();
            hb.UseBootstrapperProgram(programType);
            hb.UseMinimalConsoleProgram();
            return hb;
        }
    }

    /// <summary>
    /// The base entry point of an application optimized for console applications with a specific bootstrap type.
    /// </summary>
    /// <typeparam name ="TProgram">The type responsible for the application bootstrap.</typeparam>
    /// <seealso cref="ProgramRoot" />
    public abstract class MinimalConsoleProgram<TProgram> : MinimalConsoleProgram where TProgram : ProgramRoot
    {
        /// <summary>
        /// Creates an <see cref="HostApplicationBuilder"/> used to set up the host.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="HostApplicationBuilder"/>.</returns>
        protected new static HostApplicationBuilder CreateHostBuilder(string[] args) => CreateHostBuilder(args, typeof(TProgram));
    }
}
