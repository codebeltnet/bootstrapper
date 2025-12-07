using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;

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

        [Fact]
        public async Task StartAsync_ShouldSuppressStatusMessages_WhenSuppressStatusMessagesIsTrue()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartup>()
                    .UseConsoleStartup<TestConsoleStartup>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartup>>().GetTestStore();
            Assert.Collection(loggerStore.Query(),
                entry => Assert.Equal("Trace: Inside RunAsync of TestConsoleStartup.", entry.Message)
            );
        }

        [Fact]
        public async Task StartAsync_ShouldLogRunAsyncStarted_WhenSuppressStatusMessagesIsFalse()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartup>()
                    .UseConsoleStartup<TestConsoleStartup>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartup>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message == "Information: RunAsync started.");
        }

        [Fact]
        public async Task StopAsync_ShouldLogRunAsyncCompleted_WhenRanToCompletionAndSuppressStatusMessagesIsFalse()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartup>()
                    .UseConsoleStartup<TestConsoleStartup>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartup>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message == "Information: RunAsync completed successfully.");
        }

        [Fact]
        public async Task StopAsync_ShouldNotLogRunAsyncCompleted_WhenSuppressStatusMessagesIsTrue()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartup>()
                    .UseConsoleStartup<TestConsoleStartup>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartup>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message == "Information: RunAsync completed successfully.");
        }

        [Fact]
        public async Task StopAsync_ShouldLogRunAsyncPrematureEnd_WhenNotRanToCompletionAndSuppressStatusMessagesIsFalse()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartupWithPrematureEnd>()
                    .UseConsoleStartup<TestConsoleStartupWithPrematureEnd>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartupWithPrematureEnd>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message.Contains("RunAsync ended prematurely"));
        }

        [Fact]
        public async Task StopAsync_ShouldNotLogRunAsyncPrematureEnd_WhenSuppressStatusMessagesIsTrue()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartupWithPrematureEnd>()
                    .UseConsoleStartup<TestConsoleStartupWithPrematureEnd>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartupWithPrematureEnd>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message.Contains("RunAsync ended prematurely"));
        }

        [Fact]
        public async Task StartAsync_ShouldLogFatalError_WhenExceptionOccursAndSuppressStatusMessagesIsFalse()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartupWithException>()
                    .UseConsoleStartup<TestConsoleStartupWithException>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartupWithException>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message.Contains("Fatal error") || entry.Message.Contains("Error"));
        }

        [Fact]
        public async Task StartAsync_ShouldNotLogFatalError_WhenExceptionOccursAndSuppressStatusMessagesIsTrue()
        {
            await using var test = HostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperStartup<TestConsoleStartupWithException>()
                    .UseConsoleStartup<TestConsoleStartupWithException>();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestConsoleStartupWithException>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message.Contains("Fatal error") && entry.Message.Contains("activating"));
        }
    }
}
