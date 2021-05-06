using Codebelt.Bootstrapper.Web;

namespace Codebelt.Bootstrapper.WebMvc
{
    /// <summary>
    /// The entry point of an application responsible for registering its <see cref="WebMvcStartup"/> partner.
    /// </summary>
    /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
    /// <seealso cref="WebProgram{TStartup}" />
    public class WebMvcProgram<TStartup> : WebProgram<TStartup> where TStartup : WebMvcStartup
    {
    }
}
