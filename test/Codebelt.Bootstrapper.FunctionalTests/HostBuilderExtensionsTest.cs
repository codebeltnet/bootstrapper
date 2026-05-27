using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Bootstrapper
{
    public class HostBuilderExtensionsTest : Test
    {
        public HostBuilderExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void UseBootstrapperLifetime_ShouldRegisterBootstrapperLifetime()
        {
            using var test = HostTestFactory.Create(services =>
            {
            }, hb =>
            {
                hb.UseBootstrapperLifetime();
            });

            var bootstrapperLifetime = test.Host.Services.GetService<IHostLifetime>();

            Assert.NotNull(bootstrapperLifetime);
            Assert.IsType<BootstrapperLifetime>(bootstrapperLifetime);
        }

        [Fact]
        public void UseBootstrapperStartup_ShouldRegisterStartupFactory()
        {
            using var test = HostTestFactory.Create(services =>
            {
            }, hb =>
            {
                hb.UseBootstrapperStartup<TestStartup>();
            });

            var startupFactory = test.Host.Services.GetService<IStartupFactory<TestStartup>>();

            Assert.NotNull(startupFactory);
            Assert.IsType<StartupFactory<TestStartup>>(startupFactory);
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldAddUserSecretsProvider_WhenEnvironmentIsLocalDevelopment()
        {
            using var host = CreateHost("LocalDevelopment", configureHostBuilder: hb => hb.UseBootstrapperEnvironmentDefaults<TestStartup>(), hostConfiguration: new Dictionary<string, string?>
            {
                ["hostBuilder:reloadConfigOnChange"] = "false"
            });

            var userSecretsProvider = Assert.Single(GetConfigurationRoot(host).Providers.OfType<FileConfigurationProvider>().Where(IsUserSecretsProvider));

            Assert.False(userSecretsProvider.Source.ReloadOnChange);
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldNotAddUserSecretsProvider_WhenEnvironmentIsNotLocalDevelopment()
        {
            using var host = CreateHost(Environments.Development, configureHostBuilder: hb => hb.UseBootstrapperEnvironmentDefaults<TestStartup>());

            Assert.Empty(GetConfigurationRoot(host).Providers.OfType<FileConfigurationProvider>().Where(IsUserSecretsProvider));
        }

        private static IHost CreateHost(string environmentName, Action<IHostBuilder>? configureHostBuilder = null, Dictionary<string, string?>? hostConfiguration = null)
        {
            var hostBuilder = new HostBuilder()
                .UseEnvironment(environmentName)
                .ConfigureHostConfiguration(builder =>
                {
                    if (hostConfiguration is not null)
                    {
                        builder.AddInMemoryCollection(hostConfiguration);
                    }
                });

            configureHostBuilder?.Invoke(hostBuilder);

            return hostBuilder.Build();
        }

        private static IConfigurationRoot GetConfigurationRoot(IHost host)
        {
            return Assert.IsAssignableFrom<IConfigurationRoot>(host.Services.GetRequiredService<IConfiguration>());
        }

        private static bool IsUserSecretsProvider(FileConfigurationProvider provider)
        {
            return string.Equals(Path.GetFileName(provider.Source.Path), "secrets.json", StringComparison.OrdinalIgnoreCase);
        }
    }
}
