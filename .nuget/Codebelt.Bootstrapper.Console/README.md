## About

An open-source family of assemblies (MIT license) that provide a uniform and consistent way of bootstraping your code with `Program.cs` paired with `Startup.cs`.

Also, common for all, is the implementation of the `IHostedService` interface; this means that all project types, including the traditional `console`, now have option for graceful shutdown should your application require this (cronjob scenarios or similar).

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## Codebelt.Bootstrapper.Console

An implementation optimized for `console` applications.

## Related Packages

* [Codebelt.Bootstrapper](https://www.nuget.org/packages/Codebelt.Bootstrapper/) 📦
* [Codebelt.Bootstrapper.Console](https://www.nuget.org/packages/Codebelt.Bootstrapper.Console/) 📦
* [Codebelt.Bootstrapper.Web](https://www.nuget.org/packages/Codebelt.Bootstrapper.Web/) 📦
* [Codebelt.Bootstrapper.Worker](https://www.nuget.org/packages/Codebelt.Bootstrapper.Worker/) 📦

### CSharp Example

An example on how to use `Codebelt.Bootstrapper.Console` in C#:

```csharp

// --- Program.cs ---

public class Program : ConsoleProgram<Startup>
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

public class Startup : ConsoleStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void UseServices(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

        BootstrapperLifetime.OnApplicationStartedCallback = () => logger.LogInformation("Started");
        BootstrapperLifetime.OnApplicationStoppingCallback = () =>
        {
            logger.LogWarning("Stopping and cleaning ..");
            Thread.Sleep(TimeSpan.FromSeconds(5)); // simulate graceful shutdown
            logger.LogWarning(".. done!");
        };
        BootstrapperLifetime.OnApplicationStoppedCallback = () => logger.LogCritical("Stopped");
    }

    public async override Task RunAsync(CancellationToken cancellationToken)
    {
        for (int dots = 0; dots <= 5; ++dots)
        {
            if (cancellationToken.IsCancellationRequested) { return; }
            System.Console.Write($"\rFire and forget {Generate.FixedString('.', dots)}");
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
        }

        System.Console.WriteLine("\nDone and done!");
    }
}

```
