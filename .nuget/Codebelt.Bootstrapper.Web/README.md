## About

An open-source family of assemblies (MIT license) that provide a uniform and consistent way of bootstraping your code with `Program.cs` paired with `Startup.cs`.

Also, common for all, is the implementation of the `IHostedService` interface; this means that all project types, including the traditional `console`, now have option for graceful shutdown should your application require this (cronjob scenarios or similar).

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## Codebelt.Bootstrapper.Web

An implementation optimized for `web`, `webapi`, `webapp`, `razor`, `mvc` applications.

## Related Packages

* [Codebelt.Bootstrapper](https://www.nuget.org/packages/Codebelt.Bootstrapper/) 📦
* [Codebelt.Bootstrapper.Console](https://www.nuget.org/packages/Codebelt.Bootstrapper.Console/) 📦
* [Codebelt.Bootstrapper.Web](https://www.nuget.org/packages/Codebelt.Bootstrapper.Web/) 📦
* [Codebelt.Bootstrapper.Worker](https://www.nuget.org/packages/Codebelt.Bootstrapper.Worker/) 📦

### CSharp Example

An example on how to use `Codebelt.Bootstrapper.Web` in C#:

```csharp

// --- Program.cs ---

public class Program : WebProgram<Startup>
{
    static async Task Main(string[] args)
    {
        await CreateHostBuilder(args)
            .Build()
            .RunAsync()
            .ConfigureAwait(false);
    }
}

// --- Startup.cs ---

public class Startup : WebStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void ConfigurePipeline(IApplicationBuilder app)
    {
        if (Environment.IsLocalDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
    }
}

```
