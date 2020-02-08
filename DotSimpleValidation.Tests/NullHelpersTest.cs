using DotSimpleValidation.Tests.Examples;
using DotSimpleValidation.Tests.Usage;
using Xunit;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class NullableTests
    {
        [Fact]
        public void Should_Throw_On_Not_Nullable_Field()
        {
            Assert.Throws<ValidationException>(() => new NullableTestClass(null, null));
        }
        
        [Fact]
        public void Should_See_Nullable_Field()
        {
            var partlyNullable = new NullableTestClass(null, "not null");
            Assert.Null(partlyNullable.NullableField);
            Assert.NotNull(partlyNullable.NotNullableField);
        }

        [Fact]
        public void Should_Throw_On_Nullable_Reference_Type()
        {
            Assert.Throws<ValidationException>(() => ((string?) null).NotNull());
        }

        [Fact]
        public void Should_Throw_On_Nullable_Value_Type()
        {
            Assert.Throws<ValidationException>(() => ((int?) null).NotNull().MustBe(Between<int>(1, 2)));
        }
    }
}