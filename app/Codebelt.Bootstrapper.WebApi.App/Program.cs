using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebApi.App
{
    public class Program : WebApiProgram<Startup>
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
