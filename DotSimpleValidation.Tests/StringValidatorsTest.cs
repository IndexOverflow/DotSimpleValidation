using System.Text.RegularExpressions;
using Xunit;

namespace DotSimpleValidation.Tests
{
    public class StringValidatorsTest
    {
        
        [Theory]
        [InlineData("test", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789|,@-.!\\$%/?[]^{}~`_", true)]
        [InlineData("t€st", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789|,@-.!\\$%/?[]^{}~`_", false)]
        public void Should_Validate_Char_Match(string value, string allowedChars, bool expectValid)
        {
            var chars = allowedChars.ToCharArray();
            var validator = Validators.Match(chars);
            
            GenericValidatorsTest.AssertResult(validator(value), expectValid);
        }
        
        [Theory]
        [InlineData("valid", "^valid$", true)]
        [InlineData("invalid", "^valid$", false)]
        public void Should_Validate_Regex_Match(string value, string pattern, bool expectValid)
        {
            var validator = Validators.Match(new Regex(pattern));
            
            GenericValidatorsTest.AssertResult(validator(value), expectValid);
        }

        [Theory]
        [InlineData("hello", true)]
        [InlineData("", false)]
        [InlineData("         ", false)]
        public void Should_Validate_NotBlankOrEmpty(string value, bool expectValid)
        {
            var validator = Validators.NotBlankOrEmpty();
            GenericValidatorsTest.AssertResult(validator(value), expectValid);
        }

        [Theory]
        [InlineData(1,3, "2", true)]
        [InlineData(-1,-3, "-2", true)]
        [InlineData(10,30, "9", false)]
        [InlineData(10,30, "31", false)]
        public void Should_Validate_Between(int min, int max, string value, bool expectValid)
        {
            var validator = Validators.Between(min, max);
            GenericValidatorsTest.AssertResult(validator(value), expectValid);
        }

        [Theory]
        [InlineData("test", 4,4, true)]
        [InlineData("testthis", 4,0, true)]
        [InlineData("no", 3,0, false)]
        [InlineData("nonono", 2,4, false)]
        public void Should_Validate_OfLength(string value, int minLength, int maxLength, bool expectValid)
        {
            var validator = Validators.OfLength(minLength, maxLength);
            GenericValidatorsTest.AssertResult(validator(value), expectValid);
        }
    }
}