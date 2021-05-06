using Codebelt.Bootstrapper.Web;

namespace Codebelt.Bootstrapper.WebApp
{
    /// <summary>
    /// The entry point of an application responsible for registering its <see cref="WebAppStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="WebProgram{TStartup}" />
    public class WebAppProgram<TStartup> : WebProgram<TStartup> where TStartup : WebAppStartup
    {
    }
}
