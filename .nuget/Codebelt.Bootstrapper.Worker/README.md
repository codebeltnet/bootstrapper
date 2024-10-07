## About

An open-source family of assemblies (MIT license) that provide a uniform and consistent way of bootstraping your code with `Program.cs` paired with `Startup.cs`.

Also, common for all, is the implementation of the `IHostedService` interface; this means that all project types, including the traditional `console`, now have option for graceful shutdown should your application require this (cronjob scenarios or similar).

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## Codebelt.Bootstrapper.Worker

An implementation optimized for `worker` services.

## Related Packages

* [Codebelt.Bootstrapper](https://www.nuget.org/packages/Codebelt.Bootstrapper/) 📦
* [Codebelt.Bootstrapper.Console](https://www.nuget.org/packages/Codebelt.Bootstrapper.Console/) 📦
* [Codebelt.Bootstrapper.Web](https://www.nuget.org/packages/Codebelt.Bootstrapper.Web/) 📦
* [Codebelt.Bootstrapper.Worker](https://www.nuget.org/packages/Codebelt.Bootstrapper.Worker/) 📦

### CSharp Example

An example on how to use `Codebelt.Bootstrapper.Worker` in C#:

```csharp

// --- Program.cs ---

public class Program : WorkerProgram<Startup>
{
    static async Task Main(string[] args)
    {
        await CreateHostBuilder(args)
            .Build()
            .RunAsync()
            .ConfigureAwait(false);
    }
}

// --- Startup.cs ---

public class Startup : WorkerStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedService<FakeHostedService>();
    }
}

// --- FakeHostedService.cs ---

public class FakeHostedService : BackgroundService
{
    private readonly ILogger<FakeHostedService> _logger;
    private bool _gracefulShutdown;

    public FakeHostedService(ILogger<FakeHostedService> logger)
    {
        _logger = logger;

        BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogInformation("Started");
        BootstrapperLifetime.OnApplicationStoppingCallback = () =>
        {
            _gracefulShutdown = true;
            logger.LogWarning("Stopping and cleaning ..");
            Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate graceful shutdown
            logger.LogWarning(".. done!");
        };
        BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_gracefulShutdown) { return; }
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow.ToString("O"));
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }
}

```
