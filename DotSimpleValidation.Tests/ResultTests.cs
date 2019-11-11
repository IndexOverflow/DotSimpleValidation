using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class EitherTests
    {
        [Test]
        public void should_be_either_left()
        {
            Assert.That("fail".ResultMustBe(Equal("notfail")) is Result<string, string>.Invalid);
        }

        [Test]
        public void should_be_either_right()
        {
            Assert.That("success".ResultMustBe(Equal("success")) is Result<string, string>.Valid);
        }
    }
}