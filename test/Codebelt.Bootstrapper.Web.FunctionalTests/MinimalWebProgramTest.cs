using System.Threading.Tasks;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Cuemon.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Bootstrapper.Web
{
    public class MinimalWebProgramTest : Test
    {
        public MinimalWebProgramTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task ExecuteAsync_ShouldSimulateAMinimalWebApplication()
        {
            using var test = await MinimalWebHostTestFactory.RunWithHostBuilderContextAsync((context, services) => 
            {
                services.AddXunitTestLogging(TestOutput);
            }, (context, app) =>
            {
                if (context.HostingEnvironment.IsLocalDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    });
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            });

            var helloWorld = await test.Content.ReadAsStringAsync();

            Assert.Equal("Hello World!", helloWorld);
        }
    }
}
