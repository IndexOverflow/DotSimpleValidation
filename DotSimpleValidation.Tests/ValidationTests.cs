using System.Text.RegularExpressions;
using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ValidationTests
    {
        [Test]
        public void should_not_manipulate_value()
        {
            Assert.That("normal" == "normal".MustBe(NotNullOrBlank(), Match(new Regex("(normal)"))));
        }

        [Test]
        public void should_not_in_allowed_range()
        {
            Assert.Throws<ValidationException>(() => 3.MustBe(Between<int>(1, 2)));
        }

        [Test]
        public void should_be_in_allowed_range()
        {
            Assert.True("2" == "2".MustBe(Between<string>(1, 3)));
        }

        [Test]
        public void should_not_be_blank()
        {
            Assert.Throws<ValidationException>(() => " ".MustBe(NotNullOrBlank()));
        }

        [Test]
        public void should_be_not_be_equal()
        {
            Assert.Throws<ValidationException>(() => "Aa".MustBe(Equal("Bb")));
        }
    }
}