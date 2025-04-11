using System;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Bootstrapper.Console
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Returns the associated <see cref="ITestStore{T}"/> that is provided when settings up services from <see cref="ServiceCollectionExtensions.AddXunitTestLogging"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> from which to retrieve the <see cref="ITestStore{T}"/>.</param>
        /// <returns>Returns an implementation of <see cref="ITestStore{T}"/> with all logged entries expressed as <see cref="XunitTestLoggerEntry"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="logger"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="logger"/> does not contain a test store.
        /// </exception>
        public static ITestStore<XunitTestLoggerEntry> GetTestStore(this ILogger logger, Type loggerType)
        {
            return logger.GetTestStore(loggerType.FullName);
        }
    }
}
