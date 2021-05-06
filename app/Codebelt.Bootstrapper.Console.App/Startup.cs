using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Common.App
{
    public class Startup : ConsoleStartup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Run(IServiceProvider serviceProvider, ILogger logger)
        {
            BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogInformation("Started");
            BootstrapperLifetime.OnApplicationStoppingCallback = () => logger.LogWarning("Stopping");
            BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");

            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
