using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class TestConsoleStartup : ConsoleStartup
    {
        public TestConsoleStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TestConsoleStartup>>();
            logger.LogTrace($"Inside {nameof(RunAsync)} of {nameof(TestConsoleStartup)}.");
            return Task.CompletedTask;
        }
    }
}
