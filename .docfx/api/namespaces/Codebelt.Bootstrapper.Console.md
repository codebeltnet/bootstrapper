---
uid: Codebelt.Bootstrapper.Console
summary: *content
---
The `Codebelt.Bootstrapper.Console` namespace delivers the console-specific implementation of the Codebelt bootstrapper, including `ConsoleProgram<TStartup>`/`ConsoleStartup` for the conventional Program/Startup pair and `MinimalConsoleProgram` for the minimal-host model.

Use it when the host is a console application — a long-running daemon, a cron-style job, or a CLI process — and you want the `BootstrapperLifetime` callbacks (`OnApplicationStartedCallback`, `OnApplicationStoppingCallback`, `OnApplicationStoppedCallback`) to surface in your `Startup` so you can log and tear down cleanly. The minimal variant reflects over the entry assembly to discover a concrete `MinimalConsoleProgram` type, so the project keeps a clean `Program.cs` while still hosting a real application model.

If you are starting a new console application, start with `MinimalConsoleProgram` and its `CreateHostBuilder` helper — it composes `UseBootstrapperLifetime`, `UseBootstrapperEnvironmentDefaults`, `UseBootstrapperProgram`, and `UseMinimalConsoleProgram` in one call. If you are keeping the conventional `Program.cs`/`Startup.cs` split, start with `ConsoleProgram<TStartup>` and pair it with `ConsoleStartup` to register services and override `RunAsync`.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|HostApplicationBuilder|⬇️|`UseBootstrapperProgram`, `UseMinimalConsoleProgram`|
|IHostBuilder|⬇️|`UseConsoleStartup<TStartup>`|
