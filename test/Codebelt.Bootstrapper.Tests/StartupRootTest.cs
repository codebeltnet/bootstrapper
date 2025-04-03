using System.Collections.Generic;
using System.IO;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Bootstrapper
{
    public class StartupRootTest : Test
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public StartupRootTest(ITestOutputHelper output) : base(output)
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"TestKey", "TestValue"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _environment = new HostingEnvironment()
            {
                EnvironmentName = Environments.Development,
                ApplicationName = "TestApp",
                ContentRootPath = Directory.GetCurrentDirectory()
            };
        }

        [Fact]
        public void ConfigurationProperty_ShouldReturnInjectedConfiguration()
        {
            // Arrange
            var startup = new TestStartupRoot(_configuration, _environment);

            // Act
            var configuration = StartupRootUnsafeAccessor.GetConfiguration(startup);

            // Assert
            Assert.Equal(_configuration, configuration);
        }



        [Fact]
        public void EnvironmentProperty_ShouldReturnInjectedEnvironment()
        {
            // Arrange
            var startup = new TestStartupRoot(_configuration, _environment);

            // Act
            var environment = StartupRootUnsafeAccessor.GetEnvironment(startup);

            // Assert
            Assert.Equal(_environment, environment);
        }

        [Fact]
        public void ConfigureServices_ShouldAddServicesToServiceCollection()
        {
            // Arrange
            var services = new ServiceCollection();
            var startup = new TestStartupRoot(_configuration, _environment);

            // Act
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var testService = serviceProvider.GetService<string>();

            // Assert
            Assert.Equal("TestService", testService);
        }
    }
}
