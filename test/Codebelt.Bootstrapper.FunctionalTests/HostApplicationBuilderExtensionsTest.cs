using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace Codebelt.Bootstrapper
{
    public class HostApplicationBuilderExtensionsTest : Test
    {
        public HostApplicationBuilderExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void UseBootstrapperLifetime_ShouldRegisterBootstrapperLifetime()
        {
            var host = Host.CreateApplicationBuilder()
                .UseBootstrapperLifetime()
                .Build();

            var bootstrapperLifetime = host.Services.GetService<IHostLifetime>();

            Assert.NotNull(bootstrapperLifetime);
            Assert.IsType<BootstrapperLifetime>(bootstrapperLifetime);
        }
    }
}
