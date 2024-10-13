using Codebelt.Bootstrapper.Web;
using Cuemon.Extensions.Hosting;

namespace Codebelt.Bootstrapper.MinimalWebMvc.App
{
    public class Program : MinimalWebProgram
    {
        static Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsLocalDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app.RunAsync();
        }
    }
}
