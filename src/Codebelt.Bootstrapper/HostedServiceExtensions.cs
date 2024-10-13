using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Extension methods for the <see cref="IHostedService"/>.
    /// </summary>
    public static class HostedServiceExtensions
    {
        /// <summary>  
        /// Waits for the application to start and present an informational message.
        /// </summary>  
        /// <param name="hostedService">The <see cref="IHostedService"/> to extend.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task WaitForApplicationStartedAnnouncementAsync(this IHostedService hostedService, CancellationToken cancellationToken = default)
        {
            var tsc = new TaskCompletionSource();
            BootstrapperLifetime.OnApplicationStartedCallback += () => tsc.SetResult();
            await tsc.Task.ConfigureAwait(false); // give time for the host to start and present informational message
        }
    }
}
