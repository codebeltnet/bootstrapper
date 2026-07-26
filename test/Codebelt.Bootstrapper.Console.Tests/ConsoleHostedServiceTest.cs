using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Codebelt.Bootstrapper.Console;

public class ConsoleHostedServiceTest : Test
{
    public ConsoleHostedServiceTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task StartAsync_ShouldConfigureStartupImmediately_AndRunAfterApplicationStarted()
    {
        var startup = new SpyConsoleStartup();
        var logger = new TestLogger<SpyConsoleStartup>();
        await using var provider = HostedServiceTestFactory.CreateServiceProvider(services =>
        {
            services.AddSingleton<ILogger<SpyConsoleStartup>>(logger);
        });
        var applicationLifetime = new FakeHostApplicationLifetime();
        var events = new FakeHostLifetimeEvents();
        var sut = new ConsoleHostedService<SpyConsoleStartup>(new StaticStartupFactory<SpyConsoleStartup>(startup), applicationLifetime, provider, events);

        await sut.StartAsync(CancellationToken.None);

        Assert.True(startup.ConfigureConsoleWasCalled);
        Assert.False(startup.RunAsyncWasCalled);
        Assert.True(events.OnApplicationStartedCallbackAssigned);

        events.RaiseApplicationStarted();
        await applicationLifetime.WaitForStopAsync();
        await sut.StopAsync(CancellationToken.None);

        Assert.True(startup.RunAsyncWasCalled);
        Assert.True(applicationLifetime.StopApplicationWasCalled);
        Assert.Same(provider, startup.ConfigureConsoleServiceProvider);
        Assert.Same(provider, startup.RunAsyncServiceProvider);

        Assert.Collection(logger.Entries,
            entry =>
            {
                Assert.Equal(LogLevel.Information, entry.LogLevel);
                Assert.Equal("RunAsync started.", entry.Message);
            },
            entry =>
            {
                Assert.Equal(LogLevel.Information, entry.LogLevel);
                Assert.Equal("RunAsync completed successfully.", entry.Message);
            });
    }

    [Fact]
    public async Task StartAsync_ShouldLogUnableToActivate_WhenStartupFactoryReturnsNull()
    {
        var logger = new TestLogger<SpyConsoleStartup>();
        await using var provider = HostedServiceTestFactory.CreateServiceProvider(services =>
        {
            services.AddSingleton<ILogger<SpyConsoleStartup>>(logger);
        });
        var applicationLifetime = new FakeHostApplicationLifetime();
        var events = new FakeHostLifetimeEvents();
        var sut = new ConsoleHostedService<SpyConsoleStartup>(new StaticStartupFactory<SpyConsoleStartup>(null), applicationLifetime, provider, events);

        await sut.StartAsync(CancellationToken.None);

        Assert.False(events.OnApplicationStartedCallbackAssigned);
        Assert.False(applicationLifetime.StopApplicationWasCalled);

        Assert.Collection(logger.Entries,
            entry =>
            {
                Assert.Equal(LogLevel.Warning, entry.LogLevel);
                Assert.Equal($"Unable to activate an instance of {typeof(SpyConsoleStartup).FullName}.", entry.Message);
            });
    }

    private sealed class SpyConsoleStartup : ConsoleStartup
    {
        public SpyConsoleStartup() : base(new ConfigurationBuilder().Build(), new FakeHostEnvironment())
        {
        }

        public bool ConfigureConsoleWasCalled { get; private set; }

        public IServiceProvider? ConfigureConsoleServiceProvider { get; private set; }

        public bool RunAsyncWasCalled { get; private set; }

        public IServiceProvider? RunAsyncServiceProvider { get; private set; }

        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void ConfigureConsole(IServiceProvider serviceProvider)
        {
            ConfigureConsoleWasCalled = true;
            ConfigureConsoleServiceProvider = serviceProvider;
        }

        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            RunAsyncWasCalled = true;
            RunAsyncServiceProvider = serviceProvider;
            return Task.CompletedTask;
        }
    }
}
