using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker.Assets
{
    public class TestStartup : WorkerStartup
    {
        public TestStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<FakeHostedService>();
        }
    }
}
