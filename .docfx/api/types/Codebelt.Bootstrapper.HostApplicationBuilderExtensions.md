---
uid: Codebelt.Bootstrapper.HostApplicationBuilderExtensions
example:
- *content
---
The following example shows how to install `BootstrapperLifetime` and apply local-development environment defaults on a minimal-host `HostApplicationBuilder` so that user secrets from the application assembly are added to configuration when the environment is `LocalDevelopment`.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostApplicationBuilderExtensionsDemo;

public static class Program
{
    public static void Main()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.UseBootstrapperLifetime();
        builder.UseBootstrapperEnvironmentDefaults();

        var host = builder.Build();
        host.Start();

        var lifetime = host.Services.GetRequiredService<IHostLifetime>();
    }
}
```

---
uid: Codebelt.Bootstrapper.HostApplicationBuilderExtensions.UseBootstrapperLifetime
example:
- *content
---
The following example shows how to call `UseBootstrapperLifetime` on a `HostApplicationBuilder` to replace the default `IHostLifetime` with `BootstrapperLifetime`, exposing `IHostLifetimeEvents` so hosted services can subscribe to startup and shutdown callbacks.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseBootstrapperLifetimeDemo;

public static class Program
{
    public static void Main()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.UseBootstrapperLifetime();

        var host = builder.Build();
        var lifetime = host.Services.GetRequiredService<IHostLifetime>();
    }
}
```

---
uid: Codebelt.Bootstrapper.HostApplicationBuilderExtensions.UseBootstrapperEnvironmentDefaults
example:
- *content
---
The following example shows how to call `UseBootstrapperEnvironmentDefaults` on a `HostApplicationBuilder` so that, when the environment is `LocalDevelopment`, the application assembly is resolved from `IHostEnvironment.ApplicationName` and user secrets are added to the configuration builder.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.Hosting;

namespace UseBootstrapperEnvironmentDefaultsDemo;

public static class Program
{
    public static void Main()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.UseBootstrapperEnvironmentDefaults();

        var host = builder.Build();
        host.Start();
    }
}
```
