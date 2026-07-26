---
uid: Codebelt.Bootstrapper.CommandLineContext
example:
- *content
---
The following example shows how to wrap the `args` array once at the start of `Main`, then let the rest of the bootstrapper flow inspect `CommandLineContext.Args` for switches without passing the raw array around. The outcome changes when the `--debug` switch is present, so the captured arguments directly control the startup mode.

```csharp
using System;
using System.Linq;
using Codebelt.Bootstrapper;

namespace CommandLineContextDemo;

public static class Program
{
    public static void Main(string[] args)
    {
        var context = new CommandLineContext(args);

        var mode = context.Args.Contains("--debug", StringComparer.OrdinalIgnoreCase)
            ? "Debug bootstrap enabled."
            : "Standard bootstrap enabled.";

        Console.WriteLine(mode);
    }
}
```
