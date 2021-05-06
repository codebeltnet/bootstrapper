using System;
using System.Threading;

namespace Codebelt.Bootstrapper.Common.App
{
    public class Program : ConsoleProgram<Startup>
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)); // simulate cronjob that has exceeded a max. running time
            CreateHostBuilder(args, cts.Token);
        }
    }
}
