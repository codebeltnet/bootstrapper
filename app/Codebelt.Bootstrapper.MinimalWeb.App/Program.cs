using Codebelt.Bootstrapper.Web;
using Cuemon.Extensions.Hosting;

namespace Codebelt.Bootstrapper.MinimalWeb.App
{
    public class Program : MinimalWebProgram
    {
        static Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            var app = builder.Build();

            if (app.Environment.IsLocalDevelopment())
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

            return app.RunAsync();
        }
    }
}
