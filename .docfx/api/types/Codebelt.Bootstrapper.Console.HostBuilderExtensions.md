---
uid: Codebelt.Bootstrapper.Console.HostBuilderExtensions
example:
- *content
---
The following example shows how to call `UseConsoleStartup<TStartup>` on a conventional `IHostBuilder` to register a `ConsoleHostedService<TStartup>` so the host runs the startup's `RunAsync` method from a hosted service.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseConsoleStartupDemo;

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
        using var host = Host.CreateDefaultBuilder()
            .UseConsoleStartup<MyStartup>()
            .Build();

        host.Run();
    }
}
```

---
uid: Codebelt.Bootstrapper.Console.HostBuilderExtensions.UseConsoleStartup
example:
- *content
---
The following example shows how to call `UseConsoleStartup<TStartup>` on a conventional `IHostBuilder` to register the console-style hosted service that drives `TStartup.RunAsync` from inside the host.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseConsoleStartupGenericDemo;

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
        using var host = Host.CreateDefaultBuilder()
            .UseConsoleStartup<MyStartup>()
            .Build();

        host.Run();
    }
}
```
