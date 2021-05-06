using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Web.App
{
    public class Program : WebProgram<Startup>
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }
    }
}
