namespace Codebelt.Bootstrapper;

/// <summary>
/// Represents the command-line arguments available to a bootstrapper.
/// </summary>
public class CommandLineContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandLineContext"/> class
    /// with the specified command-line arguments.
    /// </summary>
    /// <param name="args">
    /// The command-line arguments to expose through <see cref="Args"/>.
    /// If <see langword="null"/>, <see cref="Args"/> is initialized to an empty array.
    /// </param>
    public CommandLineContext(string[] args)
    {
        Args = args ?? [];
    }

    /// <summary>
    /// Gets the command-line arguments associated with this context.
    /// </summary>
    /// <value>
    /// The array supplied to the constructor, or an empty array when the supplied value was
    /// <see langword="null"/>. The supplied array is retained without creating a copy.
    /// </value>
    public string[] Args { get; }
}
