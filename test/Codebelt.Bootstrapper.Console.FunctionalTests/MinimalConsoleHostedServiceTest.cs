using System;
using System.Diagnostics;
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
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;
                Debug.WriteLine($"Unhandled exception: {exception?.Message}");
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Debug.WriteLine($"Unobserved task exception: {e.Exception.Message}");
                e.SetObserved();
            };
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

            //var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            //if (SynchronizationContext.Current == null)
            //{
            //    // normal ASP.Net Core environment does not have a synchronization context, 
            //    // no problem with await here, it will be executed on the thread pool
            //    await test.Host.StartAsync();
            //    await test.Host.WaitForShutdownAsync();
            //}
            //else
            //{
            //    // xunit uses it's own SynchronizationContext that allows a maximum thread count
            //    // equal to the logical cpu count (that is 1 on our single cpu build agents). So
            //    // when we're trying to await something here, the task get's scheduled to xunit's 
            //    // synchronization context, which is already at it's limit running the test thread
            //    // so we end up in a deadlock here.
            //    // solution is to run the await explicitly on the thread pool by using Task.Run
            //    Task.Run(async () =>
            //    {
            //        await test.Host.StartAsync();
            //        await test.Host.WaitForShutdownAsync();
            //    }).Wait();
            //}
            
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
