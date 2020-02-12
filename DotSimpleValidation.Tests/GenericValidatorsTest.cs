using Xunit;

namespace DotSimpleValidation.Tests
{
    public class GenericValidatorsTest
    {
        [Theory]
        [InlineData("aaa", "a", true)]
        [InlineData("abc", "ab", true)]
        [InlineData("ccc", "a", false)]
        public void Should_Validate_TrueFor(string value, string substring, bool expectValid)
        {
            var validator = Validators.TrueFor<string>(s => s.Contains(substring));
            AssertResult(validator(value), expectValid);
        }

        [Theory]
        [InlineData("a", "a", true)]
        [InlineData(1, 1, true)]
        [InlineData(true, true, true)]
        [InlineData(1.3, 1.4, false)]
        public void Should_Validate_Equal(object match, object candidate, bool expectValid)
        {
            var validator = Validators.Equal(candidate);
            AssertResult(validator(match), expectValid);
        }

        [Theory]
        [InlineData("1", "3", "2", true)]
        [InlineData("-1", "-3", "-2", true)]
        [InlineData("1", "1", "2", false)]
        [InlineData("-10", "-12", "-9", false)]
        
        [InlineData(1, 3, 2, true)]
        [InlineData(1, 1, 2, false)]
        
        [InlineData(10000000000000000000L, 12000000000000000000L, 11111111111111111111L, true)]
        [InlineData(10000000000000000000L, 11111111111111111111L, 12000000000000000000L, false)]
        
        [InlineData(1.1, 1.3, 1.2, true)]
        [InlineData(10.5, 11.4, 7.3, false)]
        [InlineData(10.5, 11.4, 13.3, false)]
        public void Should_Validate_Between(object min, object max, object candidate, bool expectValid)
        {
            switch (candidate)
            {
                case string asString:
                    AssertResult(Validators.Between((string) min, (string) max)(asString), expectValid);
                    break;
                case int asInteger:
                    AssertResult(Validators.Between<int>((int) min, (int) max)(asInteger), expectValid);
                    break;
                case long asLong:
                    AssertResult(Validators.Between<long>((long) min, (long) max)(asLong), expectValid);
                    break;
                case double asDouble:
                    AssertResult(Validators.Between<double>((double) min, (double) max)(asDouble), expectValid);
                    break;
            }
        }


        public static void AssertResult<T1, T2>(Result<T1, T2> result, bool expectValid)
        {
            if (expectValid)
            {
                Assert.IsType<Result<T1, T2>.Valid>(result);
            }
            else
            {
                Assert.IsType<Result<T1, T2>.Invalid>(result);
            }
        }
    }
}