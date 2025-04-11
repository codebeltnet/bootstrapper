using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Assets
{
    public class TestStartupRoot : StartupRoot
    {
        public TestStartupRoot(IConfiguration configuration, IHostEnvironment environment)
            : base(configuration, environment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // Add test services here
            services.AddSingleton<string>("TestService");
        }
    }
}
