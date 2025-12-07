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

        [Fact]
        public async Task StartAsync_ShouldSuppressStatusMessages_WhenSuppressStatusMessagesIsTrue()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgram))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgram>>().GetTestStore();
            Assert.Collection(loggerStore.Query(),
                entry => Assert.Equal("Trace: Inside RunAsync of TestMinimalConsoleProgram.", entry.Message)
            );
        }

        [Fact]
        public async Task StartAsync_ShouldLogRunAsyncStarted_WhenSuppressStatusMessagesIsFalse()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgram))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgram>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message == "Information: RunAsync started.");
        }

        [Fact]
        public async Task StopAsync_ShouldLogRunAsyncCompleted_WhenRanToCompletionAndSuppressStatusMessagesIsFalse()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgram))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgram>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message == "Information: RunAsync completed successfully.");
        }

        [Fact]
        public async Task StopAsync_ShouldNotLogRunAsyncCompleted_WhenSuppressStatusMessagesIsTrue()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgram))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgram>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message == "Information: RunAsync completed successfully.");
        }

        [Fact]
        public async Task StopAsync_ShouldLogRunAsyncPrematureEnd_WhenNotRanToCompletionAndSuppressStatusMessagesIsFalse()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgramWithPrematureEnd))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgramWithPrematureEnd>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message.Contains("RunAsync ended prematurely"));
        }

        [Fact]
        public async Task StopAsync_ShouldNotLogRunAsyncPrematureEnd_WhenSuppressStatusMessagesIsTrue()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgramWithPrematureEnd))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgramWithPrematureEnd>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message.Contains("RunAsync ended prematurely"));
        }

        [Fact]
        public async Task StartAsync_ShouldLogFatalError_WhenExceptionOccursAndSuppressStatusMessagesIsFalse()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgramWithException))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgramWithException>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message.Contains("Fatal error") || entry.Message.Contains("Error"));
        }

        [Fact]
        public async Task StartAsync_ShouldNotLogFatalError_WhenExceptionOccursAndSuppressStatusMessagesIsTrue()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseBootstrapperProgram(typeof(TestMinimalConsoleProgramWithException))
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<TestMinimalConsoleProgramWithException>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message.Contains("Fatal error") && entry.Message.Contains("activating"));
        }

        [Fact]
        public async Task StartAsync_ShouldLogUnableToActivateInstance_WhenProgramIsNullAndSuppressStatusMessagesIsFalse()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = false;
                });
                // Register null factory to simulate program not being created
                services.AddSingleton<IProgramFactory>(new NullProgramFactory());
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<MinimalConsoleProgram>>().GetTestStore();
            Assert.Contains(loggerStore.Query(), entry => entry.Message.Contains("Unable to activate"));
        }

        [Fact]
        public async Task StartAsync_ShouldNotLogUnableToActivateInstance_WhenProgramIsNullAndSuppressStatusMessagesIsTrue()
        {
            await using var test = MinimalHostTestFactory.Create(services =>
            {
                services.AddXunitTestLogging(TestOutput);
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    options.SuppressStatusMessages = true;
                });
                // Register null factory to simulate program not being created
                services.AddSingleton<IProgramFactory>(new NullProgramFactory());
            }, hb =>
            {
                hb.UseBootstrapperLifetime()
                    .UseMinimalConsoleProgram();
            });

            await test.Host.WaitForShutdownAsync();

            var loggerStore = test.Host.Services.GetRequiredService<ILogger<MinimalConsoleProgram>>().GetTestStore();
            Assert.DoesNotContain(loggerStore.Query(), entry => entry.Message.Contains("Unable to activate"));
        }
    }
}
