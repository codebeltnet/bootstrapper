---
uid: Codebelt.Bootstrapper.Console.ConsoleHostedService`1
example:
- *content
---
The following example shows how `ConsoleHostedService<TStartup>` is registered by `UseConsoleStartup<TStartup>` on the conventional `IHostBuilder`, and how the hosted service itself can be resolved from the host's service collection. When the host starts, the service logs a `RunAsync started.` message through `BootstrapperLogMessages`, runs the `TStartup.RunAsync` task, and then calls `IHostApplicationLifetime.StopApplication` to begin a graceful shutdown.

```csharp
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleHostedServiceDemo;

public class MyStartup : ConsoleStartup
{
    public MyStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public static class Program
{
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .UseConsoleStartup<MyStartup>()
            .Build();

        var hosted = host.Services.GetServices<IHostedService>();
        var consoleHosted = hosted.OfType<ConsoleHostedService<MyStartup>>().Single();
    }
}
```
