using System.Threading.Tasks;
using Codebelt.Bootstrapper.Web.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper.Web
{
    public class MinimalWebProgramBuilderTest : Test
    {
        public MinimalWebProgramBuilderTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task CreateHostBuilder_ShouldRegisterBootstrapperLifetime()
        {
            var builder = TestMinimalWebProgram.CreateHostBuilderAccessor([]);
            builder.WebHost.UseTestServer();

            await using var app = builder.Build();
            app.MapGet("/", () => "Hello World!");

            await app.StartAsync();
            try
            {
                var client = app.GetTestClient();

                Assert.Equal("Hello World!", await client.GetStringAsync("/"));
            }
            finally
            {
                await app.StopAsync();
            }
        }
    }
}
