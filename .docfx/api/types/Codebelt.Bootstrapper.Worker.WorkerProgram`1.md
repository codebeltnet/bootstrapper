---
uid: Codebelt.Bootstrapper.Worker.WorkerProgram`1
example:
- *content
---
The following example shows how `WorkerProgram<TStartup>` is used as the base entry point for a conventional .NET worker service. The `CreateHostBuilder` helper composes `UseBootstrapperLifetime`, `UseBootstrapperEnvironmentDefaults<TStartup>`, and `UseBootstrapperStartup<TStartup>` so the resulting host wires the `WorkerStartup` partner through the standard bootstrapper convention. The example derives a `Program : WorkerProgram<MyStartup>` so the protected `CreateHostBuilder` is reachable, then builds the host and confirms the bootstrapper lifetime and the `IStartupFactory<TStartup>` registration before letting the host run.

```csharp
using Codebelt.Bootstrapper;
using Codebelt.Bootstrapper.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerProgramDemo;

public class MyStartup : WorkerStartup
{
    public MyStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
    }
}

public class Program : WorkerProgram<MyStartup>
{
    public static void Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        var lifetime = host.Services.GetRequiredService<IHostLifetime>();
        var factory = host.Services.GetRequiredService<IStartupFactory<MyStartup>>();
        host.Run();
    }
}
```
