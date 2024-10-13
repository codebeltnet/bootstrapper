namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// Provides an interface for initializing services and middleware used by an application.
    /// </summary>
    public interface IProgramFactory
    {
        /// <summary>
        /// Provides access to an instance inheriting from <see cref="MinimalConsoleProgram"/>.
        /// </summary>
        /// <value>A reference to the object of type <see cref="MinimalConsoleProgram"/>.</value>
        MinimalConsoleProgram Instance { get; }
    }
}
