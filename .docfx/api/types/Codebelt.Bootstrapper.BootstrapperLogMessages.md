---
uid: Codebelt.Bootstrapper.BootstrapperLogMessages
example:
- *content
---
The static `BootstrapperLogMessages` class is the host's source of truth for the four lifecycle events that `ConsoleHostedService<TStartup>` and `MinimalConsoleHostedService` log through `Decorator.EncloseToExpose(logger, false)`: `RunAsyncStarted` when the run loop begins, `RunAsyncPrematureEnd` when the host stops before the run finishes, `RunAsyncCompleted` when the run finishes cleanly, and `FatalErrorActivating` when the run loop's `catch` block sees an exception. The companion `UnableToActivateInstance` extension surfaces a warning when the bootstrapper could not resolve a startup or program type. To use them from your own `ConsoleStartup` or `MinimalConsoleProgram`, build an `ILogger<T>` through your normal `ServiceCollection` configuration and wrap it with `Decorator.EncloseToExpose(logger, false)`; the resulting `IDecorator<ILogger>` exposes all five extensions through receiver-style calls. The example below wires a console logger, demonstrates the run-loop happy path, exercises the failure path, and shows the startup-resolution warning so every extension in this class is invoked in a single coherent workflow.

> [!NOTE]
> The `BootstrapperLogMessages` class is not intended to be used directly. That is why it's hidden behind the `Decorator.EncloseToExpose(logger, false)` call. The extensions are intended to be used through the `IDecorator<ILogger>` interface and only internal to the `ConsoleHostedService<TStartup>` and `MinimalConsoleHostedService` classes.

```csharp
using System;
using Codebelt.Bootstrapper;
using Cuemon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BootstrapperLogMessagesDemo;

public class BootstrapperLogMessagesDemoHost
{
    public void Run()
    {
        using var services = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .BuildServiceProvider();

        var logger = services.GetRequiredService<ILogger<BootstrapperLogMessagesDemoHost>>();
        var decorated = Decorator.EncloseToExpose(logger, false);

        decorated.RunAsyncStarted();

        try
        {
            throw new InvalidOperationException("Boom");
        }
        catch (Exception ex)
        {
            decorated.FatalErrorActivating(typeof(BootstrapperLogMessagesDemoHost).FullName!, ex);
        }

        decorated.UnableToActivateInstance(typeof(BootstrapperLogMessagesDemoHost).FullName!);
        decorated.RunAsyncPrematureEnd();
        decorated.RunAsyncCompleted();
    }
}
```
