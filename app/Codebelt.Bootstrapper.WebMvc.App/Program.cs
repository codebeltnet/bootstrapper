using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebMvc.App
{
    public class Program : WebMvcProgram<Startup>
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
