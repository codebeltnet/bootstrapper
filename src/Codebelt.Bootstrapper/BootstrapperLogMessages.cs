using System;
using Cuemon;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Provides centralized logging messages for the Bootstrapper SDK using the LoggerMessage pattern.
    /// This API supports the product infrastructure and is not intended to be used directly from your code.
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/dotnet/core/extensions/high-performance-logging</remarks>
    public static class BootstrapperLogMessages
    {
        // Information messages
        private static readonly Action<ILogger, Exception> RunAsyncStartedDefinition = LoggerMessage.Define(LogLevel.Information, new EventId(1000, nameof(RunAsyncStarted)), "RunAsync started.");
        private static readonly Action<ILogger, Exception> RunAsyncPrematureEndDefinition = LoggerMessage.Define(LogLevel.Information, new EventId(1001, nameof(RunAsyncPrematureEnd)), "RunAsync ended prematurely.");
        private static readonly Action<ILogger, Exception> RunAsyncCompletedDefinition = LoggerMessage.Define(LogLevel.Information, new EventId(1002, nameof(RunAsyncCompleted)), "RunAsync completed successfully.");

        // Warning messages
        private static readonly Action<ILogger, string, Exception> UnableToActivateInstanceDefinition = LoggerMessage.Define<string>(LogLevel.Warning, new EventId(2000, nameof(UnableToActivateInstance)), "Unable to activate an instance of {TypeFullName}.");

        // Critical messages
        private static readonly Action<ILogger, string, Exception> FatalErrorActivatingDefinition = LoggerMessage.Define<string>(LogLevel.Critical, new EventId(3000, nameof(FatalErrorActivating)), "Fatal error occurred while activating {TypeFullName}.");

        /// <summary>
        /// Logs that RunAsync has started.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public static void RunAsyncStarted(this IDecorator<ILogger> logger)
        {
            RunAsyncStartedDefinition(logger.Inner, null);
        }

        /// <summary>
        /// Logs that RunAsync ended prematurely.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public static void RunAsyncPrematureEnd(this IDecorator<ILogger> logger)
        {
            RunAsyncPrematureEndDefinition(logger.Inner, null);
        }

        /// <summary>
        /// Logs that RunAsync completed successfully.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public static void RunAsyncCompleted(this IDecorator<ILogger> logger)
        {
            RunAsyncCompletedDefinition(logger.Inner, null);
        }

        /// <summary>
        /// Logs an inability to activate the specified type.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="typeFullName">The full name of the type that couldn't be activated.</param>
        public static void UnableToActivateInstance(this IDecorator<ILogger> logger, string typeFullName)
        {
            UnableToActivateInstanceDefinition(logger.Inner, typeFullName, null);
        }

        /// <summary>
        /// Logs a fatal error that occurred while activating a type.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="typeFullName">The full name of the type being activated.</param>
        /// <param name="exception">The exception that occurred.</param>
        public static void FatalErrorActivating(this IDecorator<ILogger> logger, string typeFullName, Exception exception)
        {
            FatalErrorActivatingDefinition(logger.Inner, typeFullName, exception);
        }
    }
}
