using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Bootstrapper.Console
{
    public class MinimalConsoleHostedServiceTest : Test
    {
        public MinimalConsoleHostedServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task StartAsync_ShouldInvokeRunAsyncInTestConsoleStartup()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgram))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgram>>().GetTestStore();
            Assert.Collection(loggerStore.Query(),
                entry => Assert.Equal("Information: RunAsync started.", entry.Message),
                entry => Assert.Equal("Trace: Inside RunAsync of TestMinimalConsoleProgram.", entry.Message),
                entry => Assert.Equal("Information: RunAsync completed successfully.", entry.Message)
            );
        }
    }
}
