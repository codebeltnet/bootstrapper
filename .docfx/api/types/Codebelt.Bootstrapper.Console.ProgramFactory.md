---
uid: Codebelt.Bootstrapper.Console.ProgramFactory
example:
- *content
---
The following example shows how `ProgramFactory` activates a derived `MinimalConsoleProgram` from the entry assembly. The factory is what `UseBootstrapperProgram` registers, so consumer code resolves `IProgramFactory.Instance` (and can cast the registered service to `ProgramFactory` to confirm the concrete type).

```csharp
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProgramFactoryDemo;

public class MyProgram : MinimalConsoleProgram
{
    public override Task RunAsync(System.IServiceProvider serviceProvider, CancellationToken cancellationToken)
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
        var factory = (ProgramFactory)host.Services.GetRequiredService<IProgramFactory>();
        var program = factory.Instance;
    }
}
```
