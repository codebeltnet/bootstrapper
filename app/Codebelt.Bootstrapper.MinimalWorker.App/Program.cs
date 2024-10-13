using Codebelt.Bootstrapper.Worker;

namespace Codebelt.Bootstrapper.MinimalWorker.App
{
    public class Program : MinimalWorkerProgram
    {
        static Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            builder.Services.AddHostedService<FakeHostedService>();

            var host = builder.Build();
            return host.RunAsync();
        }
    }
}