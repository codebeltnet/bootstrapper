---
uid: Codebelt.Bootstrapper.Console.HostApplicationBuilderExtensions
example:
- *content
---
The following example shows how to compose `UseBootstrapperProgram` and `UseMinimalConsoleProgram` on a `HostApplicationBuilder` to wire a `MinimalConsoleProgram` implementation into the host. The `ProgramFactory` then discovers the derived `MinimalConsoleProgram` in the entry assembly and the `MinimalConsoleHostedService` runs its `RunAsync` once the host is fully started.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MinimalConsoleHostDemo;

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
        var hosted = host.Services.GetServices<IHostedService>();
    }
}
```

---
uid: Codebelt.Bootstrapper.Console.HostApplicationBuilderExtensions.UseBootstrapperProgram
example:
- *content
---
The following example shows how to call `UseBootstrapperProgram` on a `HostApplicationBuilder` to register a `ProgramFactory` that resolves a `MinimalConsoleProgram`-derived type at runtime.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseBootstrapperProgramDemo;

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

        using var host = builder.Build();
        var factory = host.Services.GetRequiredService<IProgramFactory>();
    }
}
```

---
uid: Codebelt.Bootstrapper.Console.HostApplicationBuilderExtensions.UseMinimalConsoleProgram
example:
- *content
---
The following example shows how to call `UseMinimalConsoleProgram` on a `HostApplicationBuilder` to register the `MinimalConsoleHostedService` so the run loop is driven from a single hosted service.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseMinimalConsoleProgramDemo;

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
        var hosted = host.Services.GetServices<IHostedService>();
    }
}
```
