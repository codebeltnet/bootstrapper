namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// Provides an interface for initializing services and middleware used by an application.
    /// </summary>
    /// <typeparam name="TStartup">The type to create.</typeparam>
    public interface IStartupFactory<out TStartup> where TStartup : StartupRoot
    {
        /// <summary>
        /// Provides access to an instance of the specified <typeparamref name="TStartup"/>.
        /// </summary>
        /// <value>A reference to the object of type <see cref="StartupRoot"/>.</value>
        TStartup Instance { get; }
    }
}
