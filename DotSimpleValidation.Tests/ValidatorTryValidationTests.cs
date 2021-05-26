using System.Text.RegularExpressions;
using Xunit;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ValidatorTryValidationTests
    {
        [Fact]
        public void Should_See_False_When_Trying_NullString()
        {
            string? shouldBeNull = null;
            Assert.False(Validator.TryValidation(shouldBeNull, out _, Equal("test")!));
        }
        
        [Fact]
        public void Should_See_False_When_Trying_InvalidString()
        {
            const string badString = "evil";
            
            Assert.False
            (
                Validator.TryValidation
                (
                    badString, 
                    out var notValidated, 
                    Equal("good")
                )
            );
            
            Assert.Null(notValidated);
        }

        [Fact]
        public void Should_See_True_When_Trying_Valid_String()
        {
            const string goodString = "good";
            Assert.True(Validator.TryValidation(goodString, out var validString, Equal("good")));
            Assert.Equal("good", validString);
        }

        [Fact]
        public void Should_See_True_When_Trying_Valid_Int()
        {
            const int unknownNum = 123;
            Assert.True
            (
                Validator.TryValidation
                (
                    unknownNum,
                    out var validNum,
                    Between<int>(122, 124)
                )
            );
            
            Assert.Equal(123, validNum);
        }
    }
}