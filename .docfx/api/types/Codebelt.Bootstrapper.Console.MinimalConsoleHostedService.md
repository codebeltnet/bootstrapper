---
uid: Codebelt.Bootstrapper.Console.MinimalConsoleHostedService
example:
- *content
---
The following example shows how `MinimalConsoleHostedService` is registered by `UseMinimalConsoleProgram` on a `HostApplicationBuilder`. When the host starts, the service subscribes to `IHostLifetimeEvents.OnApplicationStartedCallback`, runs the `MinimalConsoleProgram.RunAsync` task, and then calls `IHostApplicationLifetime.StopApplication` to begin a graceful shutdown. The example resolves the hosted service from the host's service collection to confirm the registration.

```csharp
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MinimalConsoleHostedServiceDemo;

public class MyProgram : MinimalConsoleProgram
{
    public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public static class Program
{
    public static void Main()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.UseBootstrapperProgram(typeof(MyProgram));
        builder.UseMinimalConsoleProgram();

        using var host = builder.Build();
        var hosted = host.Services.GetServices<IHostedService>().OfType<MinimalConsoleHostedService>().Single();
    }
}
```
