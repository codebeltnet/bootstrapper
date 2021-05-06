using Codebelt.Bootstrapper.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebMvc
{
    public abstract class WebMvcStartup : WebStartup
    {
        protected WebMvcStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
