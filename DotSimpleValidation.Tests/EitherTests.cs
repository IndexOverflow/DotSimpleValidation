using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class EitherTests
    {
        [Test]
        public void should_be_either_left()
        {
            Assert.That("fail".EitherMustBe(Equal("notfail")) is Either<string, string>.Invalid);
        }

        [Test]
        public void should_be_either_right()
        {
            Assert.That("success".EitherMustBe(Equal("success")) is Either<string, string>.Valid);
        }
    }
}