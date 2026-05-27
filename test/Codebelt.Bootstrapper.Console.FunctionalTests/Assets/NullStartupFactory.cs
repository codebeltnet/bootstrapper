namespace Codebelt.Bootstrapper.Console.Assets
{
    public class NullStartupFactory<TStartup> : global::Codebelt.Bootstrapper.IStartupFactory<TStartup> where TStartup : global::Codebelt.Bootstrapper.StartupRoot
    {
        public TStartup Instance => null;
    }
}
