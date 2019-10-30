using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ValidatorTests
    {

        [Test]
        public void should_create_uri_with_creatable_using()
        {
            Assert.That("https://github.com" == "https://github.com".MustBe(CreatableUsing((s => new Uri(s)))));
        }
        
        [Test]
        public void should_fail_with_creatable_using()
        {
            Assert.Throws<ValidationException>(() => "I-AM-NOT-AN-URI".MustBe(CreatableUsing((s => new Uri(s)))));
        }
        
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

        [Test]
        public void should_not_create_someclass()
        {
            Assert.Throws<ValidationException>(() => new SomeClass("okay123", 4, null));
        }

        private class SomeClass
        {
            public string ValidDataString { get; }
            public int ValidNumber { get; }
            public string SortaOptional { get; }

            public SomeClass(string data, int aNumber, string message)
            {
                ValidDataString = data.MustBe(Match(new Regex("([a-zA-Z0-9])")));
                ValidNumber = aNumber.MustBe(Between<int>(1, 10));
                SortaOptional = message.MustBe(NotNullOrBlank());
            }
        }
    }
}