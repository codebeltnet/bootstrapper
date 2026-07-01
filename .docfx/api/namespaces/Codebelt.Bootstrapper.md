---
uid: Codebelt.Bootstrapper
summary: *content
---
If you want a single .NET host that listens to SIGTERM/Ctrl+C, exposes a conventional Program.cs/Startup.cs split, and applies the same lifetime and environment defaults across console, web, and worker projects, the `Codebelt.Bootstrapper` namespace is the cross-cutting layer to start from. The namespace defines `BootstrapperLifetime` (which complements the default `Microsoft.Extensions.Hosting.Internal.ConsoleLifetime`), a `StartupRoot` partner for the conventional Program/Startup pair, and the shared `UseBootstrapperLifetime` / `UseBootstrapperStartup` / `UseBootstrapperEnvironmentDefaults` extension methods that the rest of the family (`Codebelt.Bootstrapper.Console`, `Codebelt.Bootstrapper.Web`, and `Codebelt.Bootstrapper.Worker`) compose on top of.

Choose this surface when you need a uniform bootstrap regardless of project type, or when a console/worker/web project should share the same lifetime callbacks and the same conventional startup shape. If you are on the minimal-host model, start with `UseBootstrapperLifetime` on `IHostApplicationBuilder` to install `BootstrapperLifetime` together with `IHostLifetimeEvents` so a hosted service can react to startup and shutdown. If you are on the conventional Program/Startup pair, start with `UseBootstrapperStartup<TStartup>` on `IHostBuilder` to wire a `StartupRoot` partner into the service collection. For local-development user secrets, reach for `UseBootstrapperEnvironmentDefaults` (or its `TStartup` overload) so secrets are added to configuration automatically.

[!INCLUDE [availability-default](../../includes/availability-default.md)]

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|HostApplicationBuilder|⬇️|`UseBootstrapperLifetime`, `UseBootstrapperEnvironmentDefaults`|
|IHostBuilder|⬇️|`UseBootstrapperLifetime`, `UseBootstrapperStartup<TStartup>`, `UseBootstrapperEnvironmentDefaults<TStartup>`|
|IHostedService|⬇️|`WaitForApplicationStartedAnnouncementAsync`|
|IDecorator<ILogger>|⬇️|`RunAsyncStarted`, `RunAsyncPrematureEnd`, `RunAsyncCompleted`, `UnableToActivateInstance`, `FatalErrorActivating`|
