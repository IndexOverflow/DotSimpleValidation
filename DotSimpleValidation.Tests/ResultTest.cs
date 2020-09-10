using Xunit;
using Xunit.Sdk;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ResultTest
    {
        [Fact]
        public void Should_Be_Result_Valid()
        {
            var validResult = "success".ResultMustBe(Equal("success"));
            Assert.IsType<Result<string, string>.Valid>(validResult);
            Assert.True(validResult.IsValid);
        }

        [Fact]
        public void Should_Be_Result_Invalid()
        {
            Assert.IsType<Result<string, string>.Invalid>("fail".ResultMustBe(Equal("notfail")));
        }

        [Fact]
        public void Should_See_Data_On_Result_Valid()
        {
            var validResult = "success".ResultMustBe(Equal("success")) as Result<string, string>.Valid;

            Assert.NotNull(validResult);
            Assert.Equal("success", validResult!.Data);
        }
        
        [Fact]
        public void Should_Be_Valid_Result()
        {
            var result = "abc".IsValid(Match("abc")).IfInvalid(123);
            Assert.True(result.IsValid);
            Assert.IsType<Result<int,string>.Valid>(result);
        }
        
        [Fact]
        public void Should_Have_Specified_Type_On_Invalid()
        {
            var result = true
                .IsValid(Equal(false))
                .IfInvalid("true isn't false!");
            
            Assert.False(result.IsValid);
            
            Assert.IsType<Result<string,bool>.Invalid>(result);
            var invalid = (Result<string, bool>.Invalid) result;
            
            Assert.IsType<string>(invalid.Error);
            Assert.Equal("true isn't false!", invalid.Error);
        }
    }
}