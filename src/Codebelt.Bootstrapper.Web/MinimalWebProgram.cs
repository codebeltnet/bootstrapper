﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Web
{
    /// <summary>
    /// The base entry point of an application optimized for web applications.
    /// </summary>
    public abstract class MinimalWebProgram : ProgramRoot
    {
        /// <summary>
        /// Creates an <see cref="HostApplicationBuilder"/> used to set up the host.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The initialized <see cref="HostApplicationBuilder"/>.</returns>
        protected static WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            return WebApplication.CreateBuilder(args)
                .UseBootstrapperLifetime();
        }
    }
}