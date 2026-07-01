---
uid: Codebelt.Bootstrapper.Web
summary: *content
---
The `Codebelt.Bootstrapper.Web` namespace is the web-specific implementation of the Codebelt bootstrapper. It adds `WebProgram<TStartup>`/`WebStartup` for the conventional ASP.NET Core Program/Startup pair and `MinimalWebProgram` for the minimal-host model.

Use it when the host is an ASP.NET Core web application — WebAPI, webapp, or MVC — and you want the same `BootstrapperLifetime` callbacks that console and worker applications use, applied to the web request pipeline. `WebStartup.ConfigurePipeline` is the entry point for the conventional pair and is wired up automatically by `WebHostBuilderExtensions.UseStartup<TStartup>` through the trivial `Configure` shim.

If you are starting a new web application, start with `MinimalWebProgram` and its `CreateHostBuilder` helper, then call `hb.UseBootstrapperLifetime()` to keep the lifetime consistent with the rest of the Codebelt family. If you are keeping the conventional Program/Startup split, start with `WebProgram<TStartup>` and pair it with `WebStartup` to define your service registration and request pipeline.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|WebApplicationBuilder|⬇️|`UseBootstrapperLifetime`, `UseBootstrapperEnvironmentDefaults`|
|IHostBuilder|⬇️|`UseBootstrapperStartup<TStartup>`, `UseBootstrapperEnvironmentDefaults<TStartup>`|
