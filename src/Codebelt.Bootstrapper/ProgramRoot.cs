namespace Codebelt.Bootstrapper
{
    /// <summary>
    /// The base entry point of an application responsible for registering its <see cref="StartupRoot"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="ProgramRoot" />
    public abstract class ProgramRoot<TStartup> : ProgramRoot where TStartup : StartupRoot
    {
    }

    /// <summary>
    /// The base entry point of an application.
    /// </summary>
    public abstract class ProgramRoot
    {
    }
}
