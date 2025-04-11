using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// Extension methods for the <see cref="IHostApplicationBuilder"/>.
    /// </summary>
    public static class HostApplicationBuilderExtensions
    {
        /// <summary>
        /// Provides an implementation of a conventional based <see cref="IProgramFactory"/>.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostApplicationBuilder" /> to configure.</param>
        /// <param name="minimalConsoleProgramType">The <see cref="Type"/> that must be assignable from <see cref="MinimalConsoleProgram"/>.</param>
        /// <returns>The same instance of the <see cref="IHostApplicationBuilder"/> for chaining.</returns>
        public static IHostApplicationBuilder UseBootstrapperProgram(this IHostApplicationBuilder hostBuilder, Type minimalConsoleProgramType)
        {
            hostBuilder.Services.AddSingleton<IProgramFactory>(new ProgramFactory(minimalConsoleProgramType));
            return hostBuilder;
        }

        /// <summary>
        /// Provides an implementation of <see cref="IHostedService"/> that has the role of a console application.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostApplicationBuilder" /> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostApplicationBuilder"/> for chaining.</returns>
        public static IHostApplicationBuilder UseMinimalConsoleProgram(this IHostApplicationBuilder hostBuilder)
        {
            hostBuilder.Services.AddHostedService<MinimalConsoleHostedService>();
            return hostBuilder;
        }
    }
}
