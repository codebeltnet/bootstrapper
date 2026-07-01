---
uid: Codebelt.Bootstrapper.HostBuilderExtensions
example:
- *content
---
The following example shows how to install `BootstrapperLifetime`, register a conventional `StartupRoot` partner through `UseBootstrapperStartup<TStartup>`, and apply local-development environment defaults on a conventional `IHostBuilder` produced by `Host.CreateDefaultBuilder`.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostBuilderExtensionsDemo;

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
        var host = Host.CreateDefaultBuilder()
            .UseBootstrapperLifetime()
            .UseBootstrapperStartup<MyStartup>()
            .UseBootstrapperEnvironmentDefaults<MyStartup>()
            .Build();

        var lifetime = host.Services.GetRequiredService<IHostLifetime>();
        var factory = host.Services.GetRequiredService<IStartupFactory<MyStartup>>();
        host.Run();
    }
}
```

---
uid: Codebelt.Bootstrapper.HostBuilderExtensions.UseBootstrapperLifetime
example:
- *content
---
The following example shows how to call `UseBootstrapperLifetime` on an `IHostBuilder` to install `BootstrapperLifetime` so the conventional host also exposes `IHostLifetimeEvents` to hosted services.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseBootstrapperLifetimeHostBuilderDemo;

public static class Program
{
    public static void Main()
    {
        using var host = Host.CreateDefaultBuilder()
            .UseBootstrapperLifetime()
            .Build();

        var lifetime = host.Services.GetRequiredService<IHostLifetime>();
    }
}
```

---
uid: Codebelt.Bootstrapper.HostBuilderExtensions.UseBootstrapperStartup
example:
- *content
---
The following example shows how to call `UseBootstrapperStartup<TStartup>` on an `IHostBuilder` to register a `StartupRoot` partner. The `IStartupFactory<TStartup>` registered here produces the `TStartup` instance that consumers resolve from the service container.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseBootstrapperStartupDemo;

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

        var factory = host.Services.GetRequiredService<IStartupFactory<MyStartup>>();
        var startup = factory.Instance;
    }
}
```

---
uid: Codebelt.Bootstrapper.HostBuilderExtensions.UseBootstrapperEnvironmentDefaults
example:
- *content
---
The following example shows how to call `UseBootstrapperEnvironmentDefaults<TStartup>` on an `IHostBuilder` so that, when the environment is `LocalDevelopment`, user secrets for the `TStartup` type are added to the application's configuration. The first call sets the environment to `LocalDevelopment` so the addition is observable; the second call leaves the default `Development` environment to demonstrate the no-op path.

```csharp
using Codebelt.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UseBootstrapperEnvironmentDefaultsHostBuilderDemo;

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
        using var localHost = Host.CreateDefaultBuilder()
            .UseEnvironment("LocalDevelopment")
            .UseBootstrapperEnvironmentDefaults<MyStartup>()
            .Build();

        using var defaultHost = Host.CreateDefaultBuilder()
            .UseEnvironment(Environments.Development)
            .UseBootstrapperEnvironmentDefaults<MyStartup>()
            .Build();

        localHost.Start();
        defaultHost.Start();
    }
}
```
