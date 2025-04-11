using Codebelt.Extensions.Xunit.Hosting;

namespace Codebelt.Bootstrapper.Console.Assets
{
    public class ManualMinimalHostFixture : MinimalHostFixture
    {
        public ManualMinimalHostFixture()
        {
            HostRunnerCallback = host => { };
        }
    }
}
