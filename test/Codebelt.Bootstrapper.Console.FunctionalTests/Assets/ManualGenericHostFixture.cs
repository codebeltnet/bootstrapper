using Codebelt.Extensions.Xunit.Hosting;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class ManualGenericHostFixture : GenericHostFixture
    {
        public ManualGenericHostFixture()
        {
            HostRunnerCallback = host => { };
        }
    }
}
