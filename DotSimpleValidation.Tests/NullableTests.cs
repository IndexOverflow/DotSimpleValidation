using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class NullableTests
    {
        [Test]
        public void should_throw_on_nullable_field()
        {
            Assert.Throws<ValidationException>(() => new NullableTestClass(null));
        }
        
        [Test]
        public void should_throw_on_nullable_reference_type()
        {
            string? test = null;
            Assert.Throws<ValidationException>(() => test.NotNull());
        }
        
        [Test]
        public void should_throw_on_nullable_value_type()
        {
            int? test = null;
            Assert.Throws<ValidationException>(() => test.NotNull().MustBe(Between<int>(1,2)));
        }                
    }
}