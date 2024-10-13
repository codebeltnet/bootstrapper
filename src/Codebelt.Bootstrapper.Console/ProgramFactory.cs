using System;
using System.Linq;
using System.Reflection;
using Cuemon;
using Cuemon.Collections.Generic;

namespace Codebelt.Bootstrapper.Console
{
    /// <summary>
    /// The default implementation of <see cref="IProgramFactory"/>.
    /// </summary>
    public class ProgramFactory : IProgramFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramFactory"/> class.
        /// </summary>
        /// <param name="minimalConsoleProgramType"></param>
        public ProgramFactory(Type minimalConsoleProgramType)
        {
            Validator.ThrowIfNotContainsType(minimalConsoleProgramType, Arguments.ToArrayOf(typeof(MinimalConsoleProgram)), $"{nameof(minimalConsoleProgramType)} is not assignable from MinimalConsoleProgram.");
            var candidate = Decorator.Enclose(minimalConsoleProgramType).GetDerivedTypes(Assembly.GetEntryAssembly()).FirstOrDefault();
            Instance = Activator.CreateInstance(candidate, true) as MinimalConsoleProgram;
        }

        /// <summary>
        /// Provides access to an instance inheriting from <see cref="MinimalConsoleProgram"/>.
        /// </summary>
        /// <value>A reference to the object of type <see cref="MinimalConsoleProgram"/>.</value>
        public MinimalConsoleProgram Instance { get; }
    }
}
