using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Worker.App
{
    public class Program : WorkerProgram<Startup>
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                .Build()
                .RunAsync()
                .ConfigureAwait(false);
        }
    }
}
