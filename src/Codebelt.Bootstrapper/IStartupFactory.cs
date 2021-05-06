using Microsoft.Extensions.DependencyInjection;

namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Provides an interface for initializing services and middleware used by an application.
    /// </summary>
    public interface IStartupFactory
    {
        /// <summary>
        /// Creates an instance of the specified <typeparamref name="TStartup"/>.
        /// </summary>
        /// <typeparam name="TStartup">The type to create.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>A reference to the newly created object of type <see cref="StartupRoot"/>.</returns>
        TStartup CreateInstance<TStartup>(out IServiceCollection services) where TStartup : StartupRoot;
    }
}
