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
    public class ConsoleHostedServiceTest : Test
    {
        public ConsoleHostedServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task StartAsync_ShouldInvokeRunAsyncInTestConsoleStartup()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartup>()
                    .UseConsoleStartup<TestConsoleStartup>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartup>>().GetTestStore();
            Assert.Collection(loggerStore.Query(),
                entry => Assert.Equal("Information: RunAsync started.", entry.Message),
                entry => Assert.Equal("Trace: Inside RunAsync of TestConsoleStartup.", entry.Message),
                entry => Assert.Equal("Information: RunAsync completed successfully.", entry.Message)
            );
        }
    }
}
