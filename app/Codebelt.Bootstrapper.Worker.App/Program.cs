using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker.App
{
    public class Program : WorkerProgram<Startup>
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }
    }
}
