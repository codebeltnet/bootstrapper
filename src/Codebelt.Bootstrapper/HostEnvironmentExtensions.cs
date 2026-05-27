using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper
{
    internal static class HostEnvironmentExtensions
    {
        private const string LocalDevelopment = "LocalDevelopment";

        internal static bool IsLocalDevelopment(this IHostEnvironment environment)
        {
            return environment.IsEnvironment(LocalDevelopment);
        }
    }
}
