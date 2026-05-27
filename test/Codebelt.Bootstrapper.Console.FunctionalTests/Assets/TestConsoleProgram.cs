using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class TestConsoleProgram : ConsoleProgram<TestConsoleStartup>
    {
        public static IHostBuilder CreateHostBuilderAccessor(string[] args) => CreateHostBuilder(args);
    }
}
