---
uid: Codebelt.Bootstrapper.BootstrapperLifetime
example:
- *content
---
The following example shows how to resolve `BootstrapperLifetime` from the host's service container, attach a callback to the application-stopped event, and confirm the lifetime is registered after the host starts. The `BootstrapperLifetime` replaces the default `Microsoft.Extensions.Hosting.Internal.ConsoleLifetime` so that consumers can subscribe to the same `IHostLifetimeEvents` surface area used by hosted services.

```csharp
using System;
using Codebelt.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BootstrapperLifetimeDemo;

public static class Program
{
    public static void Main()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.UseBootstrapperLifetime();
        var host = builder.Build();
        host.Start();

        var lifetime = (BootstrapperLifetime)host.Services.GetRequiredService<IHostLifetime>();
        lifetime.OnApplicationStoppedCallback = () => Console.WriteLine("Stopped.");

        host.StopAsync().GetAwaiter().GetResult();
    }
}
```
