using DotSimpleValidation.Tests.Examples;
using Xunit;

namespace DotSimpleValidation.Tests.Usage
{
    public class PrimitivesTest
    {
        [Fact]
        public void Should_Create_ExampleClassB()
        {
            var mrRobot = new ExampleClassB(new Name("Elliot Alderson"), new Age(29), new Occupation("hacker"));

            Assert.Equal("Elliot Alderson", mrRobot.Name.Value);
            Assert.Equal(29, mrRobot.Age.Value);
            Assert.Equal("hacker", mrRobot.Occupation.ToString()); // alt. usage
        }

        [Fact]
        public void Should_Not_Create_ExampleClassB()
        {
            Assert.Throws<ValidationException>(() =>
                new ExampleClassB(new Name("Fernando Vera"), new Age(32), new Occupation("gangster"))
            );
        }
    }
}