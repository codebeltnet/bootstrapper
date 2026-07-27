using Codebelt.Extensions.Xunit;
using Xunit;

namespace Codebelt.Bootstrapper
{
    public class CommandLineContextTest : Test
    {
        public CommandLineContextTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Constructor_ShouldInitializeWithEmptyArray_WhenArgsIsNull()
        {
            // Act
            var context = new CommandLineContext(null);

            // Assert
            Assert.NotNull(context.Args);
            Assert.Empty(context.Args);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithProvidedArray_WhenArgsIsEmpty()
        {
            // Arrange
            var args = new string[] { };

            // Act
            var context = new CommandLineContext(args);

            // Assert
            Assert.NotNull(context.Args);
            Assert.Empty(context.Args);
            Assert.Same(args, context.Args);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithProvidedArray_WhenArgsContainsValues()
        {
            // Arrange
            var args = new[] { "arg1", "arg2", "arg3" };

            // Act
            var context = new CommandLineContext(args);

            // Assert
            Assert.NotNull(context.Args);
            Assert.Equal(3, context.Args.Length);
            Assert.Equal("arg1", context.Args[0]);
            Assert.Equal("arg2", context.Args[1]);
            Assert.Equal("arg3", context.Args[2]);
            Assert.Same(args, context.Args);
        }

        [Fact]
        public void ArgsProperty_ShouldReturnActualReference_WhenMultipleAccessesOccur()
        {
            // Arrange
            var args = new[] { "value1", "value2" };
            var context = new CommandLineContext(args);

            // Act
            var firstAccess = context.Args;
            var secondAccess = context.Args;

            // Assert
            Assert.Same(firstAccess, secondAccess);
            Assert.Same(args, firstAccess);
        }

        [Fact]
        public void Constructor_ShouldPreserveSingleArgument_WhenArgsProvidesOne()
        {
            // Arrange
            var args = new[] { "--debug" };

            // Act
            var context = new CommandLineContext(args);

            // Assert
            Assert.Single(context.Args);
            Assert.Equal("--debug", context.Args[0]);
        }
    }
}
