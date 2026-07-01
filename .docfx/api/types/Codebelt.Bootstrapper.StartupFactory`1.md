---
uid: Codebelt.Bootstrapper.StartupFactory`1
example:
- *content
---
The following example shows how `StartupFactory<TStartup>` activates a `StartupRoot` partner from the host's `IServiceCollection`, configuration, and environment, then surfaces it through the `IStartupFactory<TStartup>.Instance` property. The factory is what `UseBootstrapperStartup<TStartup>` registers, so consumer code resolves the singleton instead of constructing the startup directly.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StartupFactoryDemo;

public class MyStartup : StartupRoot
{
    public MyStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
    }
}

public static class Program
{
    public static void Main()
    {
        using var host = Host.CreateDefaultBuilder()
            .UseBootstrapperStartup<MyStartup>()
            .Build();

        var factory = (StartupFactory<MyStartup>)host.Services.GetRequiredService<IStartupFactory<MyStartup>>();
        var startup = factory.Instance;
    }
}
```
