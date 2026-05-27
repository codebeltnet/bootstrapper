using System.Threading.Tasks;
using Codebelt.Bootstrapper.Web.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Web
{
    public class WebProgramTest : Test
    {
        public WebProgramTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task CreateHostBuilder_ShouldRegisterBootstrapperLifetimeAndStartup()
        {
            var builder = TestWebProgram.CreateHostBuilderAccessor([]);
            builder.ConfigureWebHost(webBuilder => webBuilder.UseTestServer());

            using var host = builder.Build();
            await host.StartAsync();
            try
            {
                var client = host.GetTestClient();

                Assert.Equal("Hello World!", await client.GetStringAsync("/"));
            }
            finally
            {
                await host.StopAsync();
            }
        }
    }
}
