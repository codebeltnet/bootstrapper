using System;
using Cuemon;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Codebelt.Bootstrapper
{
    public class BootstrapperLogMessagesTest : Test
    {
        public BootstrapperLogMessagesTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void FatalErrorActivating_ShouldWriteCriticalEntry()
        {
            using var services = new ServiceCollection()
                .AddXunitTestLogging(TestOutput)
                .BuildServiceProvider();

            var logger = services.GetRequiredService<ILogger<BootstrapperLogMessagesTest>>();

            Decorator.EncloseToExpose(logger, false).FatalErrorActivating("Test.Type", new InvalidOperationException("Boom"));

            Assert.Contains(logger.GetTestStore().Query(), entry => entry.Message.Contains("Fatal error occurred while activating Test.Type."));
        }
    }
}
