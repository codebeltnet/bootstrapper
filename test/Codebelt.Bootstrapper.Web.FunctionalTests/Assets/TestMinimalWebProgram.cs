using Microsoft.AspNetCore.Builder;

namespace Codebelt.Bootstrapper.Web.Assets
{
    internal class TestMinimalWebProgram : MinimalWebProgram
    {
        public static WebApplicationBuilder CreateHostBuilderAccessor(string[] args) => CreateHostBuilder(args);
    }
}
