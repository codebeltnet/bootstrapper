using Codebelt.Bootstrapper.Web;
using Cuemon.Extensions.Hosting;

namespace Codebelt.Bootstrapper.MinimalWebApp.App
{
    public class Program : MinimalWebProgram
    {
        static Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (app.Environment.IsLocalDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            return app.RunAsync();
        }
    }
}
