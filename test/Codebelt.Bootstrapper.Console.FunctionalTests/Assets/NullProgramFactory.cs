namespace Codebelt.Bootstrapper.Console.Assets
{
    /// <summary>
    /// A test implementation of <see cref="IProgramFactory"/> that returns null to simulate program activation failure.
    /// </summary>
    public class NullProgramFactory : IProgramFactory
    {
        /// <summary>
        /// Gets the instance of the program, which is always null for testing purposes.
        /// </summary>
        /// <value>Always returns <c>null</c>.</value>
        public MinimalConsoleProgram Instance => null;
    }
}
