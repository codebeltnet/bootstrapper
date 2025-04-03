using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper.Assets
{
    public static class StartupRootUnsafeAccessor
    {
        [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_Configuration")]
        public static extern IConfiguration GetConfiguration(StartupRoot startup);

        [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_Environment")]
        public static extern IHostEnvironment GetEnvironment(StartupRoot startup);
    }
}
