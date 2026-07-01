---
uid: Codebelt.Bootstrapper.Web.WebProgram`1
example:
- *content
---
The following example shows how `WebProgram<TStartup>` is used as the base entry point for a conventional ASP.NET Core web application. The `CreateHostBuilder` helper composes `UseBootstrapperLifetime`, `UseBootstrapperEnvironmentDefaults<TStartup>`, and the built-in `ConfigureWebHostDefaults` so the resulting host wires `WebStartup` through the standard ASP.NET Core convention. The example derives a `Program : WebProgram<MyStartup>` so the protected `CreateHostBuilder` is reachable, then builds the host and confirms the bootstrapper's lifetime is registered before letting the host run.

```csharp
using Codebelt.Bootstrapper;
using Codebelt.Bootstrapper.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebProgramDemo;

public class MyStartup : WebStartup
{
    public MyStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void ConfigurePipeline(IApplicationBuilder app)
    {
        app.Run(async context => await context.Response.WriteAsync("Hello World!"));
    }
}

public class Program : WebProgram<MyStartup>
{
    public static void Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        var lifetime = host.Services.GetRequiredService<IHostLifetime>();
        host.Run();
    }
}
```
