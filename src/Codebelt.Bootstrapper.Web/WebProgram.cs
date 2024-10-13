using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Web
{
    /// <summary>
    /// The entry point of an application responsible for registering its <see cref="WebStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="ProgramRoot{TStartup}" />
    public class WebProgram<TStartup> : ProgramRoot<TStartup> where TStartup : WebStartup
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
        }
    }
}
