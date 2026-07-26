using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console;

internal sealed class FakeHostApplicationLifetime : IHostApplicationLifetime
{
    private readonly CancellationTokenSource _started = new();
    private readonly CancellationTokenSource _stopping = new();
    private readonly CancellationTokenSource _stopped = new();
    private readonly TaskCompletionSource<bool> _stopRequested = new(TaskCreationOptions.RunContinuationsAsynchronously);

    public FakeHostApplicationLifetime()
    {
        _started.Cancel();
    }

    public CancellationToken ApplicationStarted => _started.Token;

    public CancellationToken ApplicationStopping => _stopping.Token;

    public CancellationToken ApplicationStopped => _stopped.Token;

    public bool StopApplicationWasCalled { get; private set; }

    public Task WaitForStopAsync() => _stopRequested.Task;

    public void StopApplication()
    {
        StopApplicationWasCalled = true;
        _stopping.Cancel();
        _stopped.Cancel();
        _stopRequested.TrySetResult(true);
    }
}

internal sealed class FakeHostLifetimeEvents : IHostLifetimeEvents
{
    private Action _onApplicationStartedCallback = static () => { };
    private Action _onApplicationStoppingCallback = static () => { };
    private Action _onApplicationStoppedCallback = static () => { };

    public bool OnApplicationStartedCallbackAssigned { get; private set; }

    public Action OnApplicationStartedCallback
    {
        get => _onApplicationStartedCallback;
        set
        {
            OnApplicationStartedCallbackAssigned = true;
            _onApplicationStartedCallback = value;
        }
    }

    public Action OnApplicationStoppingCallback
    {
        get => _onApplicationStoppingCallback;
        set => _onApplicationStoppingCallback = value;
    }

    public Action OnApplicationStoppedCallback
    {
        get => _onApplicationStoppedCallback;
        set => _onApplicationStoppedCallback = value;
    }

    public void RaiseApplicationStarted()
    {
        _onApplicationStartedCallback();
    }
}

internal sealed class StaticStartupFactory<TStartup>(TStartup? instance) : IStartupFactory<TStartup> where TStartup : StartupRoot
{
    public TStartup Instance => instance!;
}

internal sealed class StaticProgramFactory(MinimalConsoleProgram? instance) : IProgramFactory
{
    public MinimalConsoleProgram Instance => instance!;
}

internal sealed class FakeHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; } = Environments.Development;

    public string ApplicationName { get; set; } = nameof(FakeHostEnvironment);

    public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();

    public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
}

internal static class HostedServiceTestFactory
{
    public static ServiceProvider CreateServiceProvider(Action<IServiceCollection>? configureServices = null, Action<ConsoleLifetimeOptions>? setup = null)
    {
        var services = new ServiceCollection();

        if (setup is null)
        {
            services.Configure<ConsoleLifetimeOptions>(_ => { });
        }
        else
        {
            services.Configure(setup);
        }

        configureServices?.Invoke(services);
        return services.BuildServiceProvider();
    }
}

internal sealed record TestLogEntry(LogLevel LogLevel, string Message);

internal sealed class TestLogger<T> : ILogger<T>
{
    private readonly List<TestLogEntry> _entries = [];

    public IReadOnlyList<TestLogEntry> Entries => _entries;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return NullScope.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _entries.Add(new TestLogEntry(logLevel, formatter(state, exception)));
    }

    private sealed class NullScope : IDisposable
    {
        public static readonly NullScope Instance = new();

        public void Dispose()
        {
        }
    }
}
