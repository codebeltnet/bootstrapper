using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;

namespace Codebelt.Bootstrapper
{
    public class HostApplicationBuilderExtensionsTest : Test
    {
        private static readonly string TestAssemblyName = typeof(TestStartup).Assembly.GetName().Name!;

        public HostApplicationBuilderExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void UseBootstrapperLifetime_ShouldRegisterBootstrapperLifetime()
        {
            var hb = Host.CreateApplicationBuilder();
            hb.UseBootstrapperLifetime();
            var host = hb.Build();

            var bootstrapperLifetime = host.Services.GetService<IHostLifetime>();

            Assert.NotNull(bootstrapperLifetime);
            Assert.IsType<BootstrapperLifetime>(bootstrapperLifetime);
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldAddUserSecretsSource_WhenEnvironmentIsLocalDevelopment()
        {
            var hb = CreateHostApplicationBuilder("LocalDevelopment", TestAssemblyName, new Dictionary<string, string?>
            {
                ["hostBuilder:reloadConfigOnChange"] = "false"
            });
            var initialSecretsSourceCount = CountUserSecretsSources(hb.Configuration.Sources);

            var result = hb.UseBootstrapperEnvironmentDefaults();

            Assert.Same(hb, result);
            var userSecretsSource = Assert.Single(hb.Configuration.Sources.OfType<FileConfigurationSource>().Where(IsUserSecretsSource));
            Assert.Equal(initialSecretsSourceCount + 1, CountUserSecretsSources(hb.Configuration.Sources));
            Assert.False(userSecretsSource.ReloadOnChange);
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldIgnoreMissingApplicationAssembly_WhenEnvironmentIsLocalDevelopment()
        {
            var hb = CreateHostApplicationBuilder("LocalDevelopment", "Codebelt.Bootstrapper.Missing.Assembly", null);
            var initialSecretsSourceCount = CountUserSecretsSources(hb.Configuration.Sources);

            var exception = Record.Exception(() => hb.UseBootstrapperEnvironmentDefaults());

            Assert.Null(exception);
            Assert.Equal(initialSecretsSourceCount, CountUserSecretsSources(hb.Configuration.Sources));
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldReturnWithoutAddingUserSecrets_WhenApplicationNameIsEmpty()
        {
            var hb = CreateHostApplicationBuilder("LocalDevelopment", TestAssemblyName, null);
            ((HostingEnvironment)hb.Environment).ApplicationName = string.Empty;
            var initialSecretsSourceCount = CountUserSecretsSources(hb.Configuration.Sources);

            var result = hb.UseBootstrapperEnvironmentDefaults();

            Assert.Same(hb, result);
            Assert.Equal(initialSecretsSourceCount, CountUserSecretsSources(hb.Configuration.Sources));
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldReturnWithoutAddingUserSecrets_WhenApplicationNameIsNull()
        {
            var hb = CreateHostApplicationBuilder("LocalDevelopment", TestAssemblyName, null);
            ((HostingEnvironment)hb.Environment).ApplicationName = null!;
            var initialSecretsSourceCount = CountUserSecretsSources(hb.Configuration.Sources);

            var result = hb.UseBootstrapperEnvironmentDefaults();

            Assert.Same(hb, result);
            Assert.Equal(initialSecretsSourceCount, CountUserSecretsSources(hb.Configuration.Sources));
        }

        [Fact]
        public void UseBootstrapperEnvironmentDefaults_ShouldReturnWithoutAddingUserSecrets_WhenEnvironmentIsNotLocalDevelopment()
        {
            var hb = CreateHostApplicationBuilder(Environments.Development, TestAssemblyName, null);
            var initialSecretsSourceCount = CountUserSecretsSources(hb.Configuration.Sources);

            var result = hb.UseBootstrapperEnvironmentDefaults();

            Assert.Same(hb, result);
            Assert.Equal(initialSecretsSourceCount, CountUserSecretsSources(hb.Configuration.Sources));
        }

        private static HostApplicationBuilder CreateHostApplicationBuilder(string environmentName, string applicationName, Dictionary<string, string?>? configuration)
        {
            var settings = new HostApplicationBuilderSettings
            {
                EnvironmentName = environmentName,
                ApplicationName = applicationName
            };
            var hostBuilder = Host.CreateApplicationBuilder(settings);
            if (configuration is not null)
            {
                hostBuilder.Configuration.AddInMemoryCollection(configuration);
            }

            return hostBuilder;
        }

        private static int CountUserSecretsSources(IEnumerable<IConfigurationSource> sources)
        {
            return sources.OfType<FileConfigurationSource>().Count(IsUserSecretsSource);
        }

        private static bool IsUserSecretsSource(FileConfigurationSource source)
        {
            return string.Equals(Path.GetFileName(source.Path), "secrets.json", StringComparison.OrdinalIgnoreCase);
        }
    }
}
