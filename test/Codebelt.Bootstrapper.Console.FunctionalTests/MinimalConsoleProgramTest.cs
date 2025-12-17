using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Codebelt.Bootstrapper.Console
{
    public class MinimalConsoleProgramTest : Test
    {
        public MinimalConsoleProgramTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateHostBuilder_CreatesHostBuilder()
        {
            var args = Array.Empty<string>();
            Assert.Throws<MissingMethodException>(() => ProgramNok.Run(args));
        }

        [Fact]
        public void CreateHostBuilder_CreatesHostBuilder_Generic()
        {
            var args = Array.Empty<string>();
            ProgramOk.Run(args);
        }
    }

    public class ProgramNok : MinimalConsoleProgram
    {
        public static void Run(string[] args, Action<IServiceCollection> serviceConfigurator = null)
        {
            var builder = CreateHostBuilder(args);
            serviceConfigurator?.Invoke(builder.Services);
            using var host = builder.Build();
            host.Run();
        }

        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class ProgramOk : MinimalConsoleProgram<ProgramOk>
    {
        public static void Run(string[] args, Action<IServiceCollection> serviceConfigurator = null)
        {
            var builder = CreateHostBuilder(args);
            serviceConfigurator?.Invoke(builder.Services);
            using var host = builder.Build();
            host.Run();
        }

        public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
