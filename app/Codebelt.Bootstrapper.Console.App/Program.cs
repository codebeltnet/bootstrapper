using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console.App
{
    public class Program : ConsoleProgram<Startup>
    {
        static Task Main(string[] args)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)); // simulate cronjob that has exceeded a max. running time
            return CreateHostBuilder(args)
                .Build().RunAsync(cts.Token);
        }
    }
}
