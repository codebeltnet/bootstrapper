using Codebelt.Bootstrapper.Web;

namespace Codebelt.Bootstrapper.WebApi
{
    /// <summary>
    /// The entry point of an application responsible for registering its <see cref="WebApiStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="WebProgram{TStartup}" />
    public class WebApiProgram<TStartup> : WebProgram<TStartup> where TStartup : WebApiStartup
    {
    }
}
