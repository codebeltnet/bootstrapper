using System.Threading.Tasks;
using Codebelt.Bootstrapper.Web.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Web
{
    public class WebStartupTest : Test
    {
        public WebStartupTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task ConfigurePipeline_ShouldSimulateAWebApplication()
        {
            using var test = await WebHostTestFactory.RunWithHostBuilderContextAsync((context, services) =>
            {
                services.AddXunitTestLogging(TestOutput);
            }, hostSetup: hb =>
            {
                hb.UseBootstrapperLifetime()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseTestServer(o => o.PreserveExecutionContext = true);
                        webBuilder.UseStartup<TestStartup>();
                    });
            });

            var helloWorld = await test.Content.ReadAsStringAsync();

            Assert.Equal("Hello World!", helloWorld);
        }
    }
}
