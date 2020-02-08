using Xunit;
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
            Assert.Equal("success", validResult.Data);
        }
    }
}