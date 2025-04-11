using System;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Provides a convenient way to be notified of host lifetime events.
    /// </summary>
    public interface IHostLifetimeEvents
    {
        /// <summary>
        /// Triggered when the application host has fully started.
        /// </summary>
        Action OnApplicationStartedCallback { get; set; }

        /// <summary>
        /// Triggered when the application host is starting a graceful shutdown.
        /// </summary>
        Action OnApplicationStoppingCallback { get; set; }

        /// <summary>
        /// Triggered when the application host has completed a graceful shutdown.
        /// </summary>
        Action OnApplicationStoppedCallback { get; set; }
    }
}
