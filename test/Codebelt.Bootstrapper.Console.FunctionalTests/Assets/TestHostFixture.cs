using System.Collections.Generic;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class TestHostFixture : ManagedHostFixture
    {
        public override void ConfigureHost(Test hostTest)
        {
            var hb = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    ConfigureCallback(config.Build(), context.HostingEnvironment);
                })
                .ConfigureServices((context, services) =>
                {
                    Configuration = context.Configuration;
                    Environment = context.HostingEnvironment;
                    ConfigureServicesCallback(services);
                })
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { HostDefaults.ApplicationKey, hostTest.CallerType.Assembly.GetName().Name }
                    });
                });
            ConfigureHostCallback(hb);
            Host = hb.Build();
        }
    }
}
