using System.Threading.Tasks;
using Codebelt.Bootstrapper.Web;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.WebApi.App
{
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
}
