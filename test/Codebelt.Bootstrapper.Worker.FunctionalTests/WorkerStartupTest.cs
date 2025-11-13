using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Worker.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Codebelt.Bootstrapper.Worker
{
    public class WorkerStartupTest : Test
    {
        public WorkerStartupTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task ExecuteAsync_ShouldSimulateAWorkerService()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestStartup>();
            }, new SelfManagedHostFixture());

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            await test.Host.StartAsync(cts.Token);
            await test.Host.WaitForShutdownAsync(cts.Token);

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<FakeHostedService>>().GetTestStore();
            Assert.Collection(loggerStore.Query(),
                entry => Assert.Equal("Information: Started", entry.Message),
                entry => Assert.Equal("Information: Worker running in iterations: 1", entry.Message),
                entry => Assert.Equal("Information: Worker running in iterations: 2", entry.Message),
                entry => Assert.Equal("Information: Worker running in iterations: 3", entry.Message),
                entry => Assert.Equal("Information: Worker running in iterations: 4", entry.Message),
                entry => Assert.Equal("Information: Worker running in iterations: 5", entry.Message),
                entry => Assert.Equal("Warning: Stopping and cleaning ..", entry.Message),
                entry => Assert.Equal("Warning: .. done!", entry.Message),
                entry => Assert.Equal("Critical: Stopped", entry.Message)
            );
        }
    }
}
