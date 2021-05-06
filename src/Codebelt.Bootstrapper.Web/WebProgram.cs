using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Web
{
    /// <summary>
    /// The entry point of an application responsible for registering its <see cref="WebStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="ProgramRoot{TStartup}" />
    public class WebProgram<TStartup> : ProgramRoot<TStartup> where TStartup : WebStartup
    {
        static WebProgram()
        {
            CreateHostBuilderCallback = args => Host.CreateDefaultBuilder(args)
                .UseBootstrapperLifetime()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        var logger = services.BuildServiceProvider().GetService<ILogger<TStartup>>();
                        if (logger != null)
                        {
                            services.AddSingleton<ILogger>(logger); // we need a non-generic variant of the logger for WebStartup/Startup
                        }
                    });
                    webBuilder.UseStartup<TStartup>();
                });
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
