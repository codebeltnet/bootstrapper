---
uid: Codebelt.Bootstrapper.Worker
summary: *content
---
If you build a .NET worker service and want the same lifetime callbacks and the same conventional Program.cs/Startup.cs split that the rest of the Codebelt family uses, the `Codebelt.Bootstrapper.Worker` namespace is your starting point. It provides `WorkerProgram<TStartup>` and `WorkerStartup` for the conventional Program/Startup pair, and `MinimalWorkerProgram` for the minimal-host model, both wired against `BootstrapperLifetime` and the shared `UseBootstrapperEnvironmentDefaults` for local-development user secrets.

Choose this namespace when the host is a `BackgroundService`, a long-running queue consumer, or any other `IHostedService` host that should react to startup and shutdown callbacks through `IHostLifetimeEvents`. The worker flavor is intentionally lightweight because worker hosts are almost always `Host.CreateApplicationBuilder` or `Host.CreateDefaultBuilder` based.

If you are starting a new worker service, start with `MinimalWorkerProgram` and its `CreateHostBuilder` helper — it composes `UseBootstrapperLifetime` and `UseBootstrapperEnvironmentDefaults` so a hosted service can react to lifetime events. If you are keeping the conventional Program/Startup split, start with `WorkerProgram<TStartup>` and pair it with `WorkerStartup` to register the worker's services.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]
