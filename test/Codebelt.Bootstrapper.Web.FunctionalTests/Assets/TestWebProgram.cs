using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Web.Assets
{
    internal class TestWebProgram : WebProgram<TestStartup>
    {
        public static IHostBuilder CreateHostBuilderAccessor(string[] args) => CreateHostBuilder(args);
    }
}
