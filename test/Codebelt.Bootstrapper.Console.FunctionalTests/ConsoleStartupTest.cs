using System.Collections.Generic;
using System.IO;
using Codebelt.Bootstrapper.Console.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;

namespace Codebelt.Bootstrapper.Console
{
    public class ConsoleStartupTest : Test
    {
        public ConsoleStartupTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void ConfigureConsole_ShouldAllowDefaultNoOpImplementation()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "TestKey", "TestValue" }
                })
                .Build();

            var environment = new HostingEnvironment
            {
                EnvironmentName = Environments.Development,
                ApplicationName = "TestApp",
                ContentRootPath = Directory.GetCurrentDirectory()
            };

            var startup = new TestConsoleStartup(configuration, environment);
            var serviceProvider = new ServiceCollection().BuildServiceProvider();

            var exception = Record.Exception(() => startup.ConfigureConsole(serviceProvider));

            Assert.Null(exception);
        }
    }
}
