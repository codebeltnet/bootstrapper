using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebApp.App
{
    public class Program : WebAppProgram<Startup>
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
