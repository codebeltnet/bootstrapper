using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Codebelt.Bootstrapper.Console;

public class MinimalConsoleHostedServiceTest : Test
{
    public MinimalConsoleHostedServiceTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task StartAsync_ShouldRunProgramAfterApplicationStarted_AndStopApplication()
    {
        var program = new SpyMinimalConsoleProgram();
        var logger = new TestLogger<SpyMinimalConsoleProgram>();
        await using var provider = HostedServiceTestFactory.CreateServiceProvider(services =>
        {
            services.AddSingleton<ILogger<SpyMinimalConsoleProgram>>(logger);
        });
        var applicationLifetime = new FakeHostApplicationLifetime();
        var events = new FakeHostLifetimeEvents();
        var sut = new MinimalConsoleHostedService(new StaticProgramFactory(program), applicationLifetime, provider, events);

        await sut.StartAsync(CancellationToken.None);

        Assert.False(program.RunAsyncWasCalled);
        Assert.True(events.OnApplicationStartedCallbackAssigned);

        events.RaiseApplicationStarted();
        await applicationLifetime.WaitForStopAsync();
        await sut.StopAsync(CancellationToken.None);

        Assert.True(program.RunAsyncWasCalled);
        Assert.True(applicationLifetime.StopApplicationWasCalled);
        Assert.Same(provider, program.RunAsyncServiceProvider);

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
    public async Task StartAsync_ShouldLogUnableToActivate_WhenProgramFactoryReturnsNull()
    {
        var logger = new TestLogger<MinimalConsoleProgram>();
        await using var provider = HostedServiceTestFactory.CreateServiceProvider(services =>
        {
            services.AddSingleton<ILogger<MinimalConsoleProgram>>(logger);
        });
        var applicationLifetime = new FakeHostApplicationLifetime();
        var events = new FakeHostLifetimeEvents();
        var sut = new MinimalConsoleHostedService(new StaticProgramFactory(null), applicationLifetime, provider, events);

        await sut.StartAsync(CancellationToken.None);
        events.RaiseApplicationStarted();
        await applicationLifetime.WaitForStopAsync();

        Assert.True(events.OnApplicationStartedCallbackAssigned);
        Assert.True(applicationLifetime.StopApplicationWasCalled);

        Assert.Collection(logger.Entries,
            entry =>
            {
                Assert.Equal(LogLevel.Warning, entry.LogLevel);
                Assert.Equal($"Unable to activate an instance of {typeof(MinimalConsoleProgram).FullName}.", entry.Message);
            });
    }

    private sealed class SpyMinimalConsoleProgram : MinimalConsoleProgram
    {
        public bool RunAsyncWasCalled { get; private set; }

        public IServiceProvider? RunAsyncServiceProvider { get; private set; }

        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            RunAsyncWasCalled = true;
            RunAsyncServiceProvider = serviceProvider;
            return Task.CompletedTask;
        }
    }
}
