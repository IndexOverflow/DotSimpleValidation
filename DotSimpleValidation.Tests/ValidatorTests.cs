using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ValidatorTests
    {
        [Test]
        public void should_be_true_for_predicate()
        {
            Assert.That("is a test" == "is a test".MustBe(TrueFor<string>((s) => s.Contains("test"))));
        }

        [Test]
        public void should_create_uri_with_creatable_using()
        {
            Assert.That("https://github.com" == "https://github.com".MustBe(CreatableUsing<string,Uri>(s => new Uri(s))));
        }
        
        [Test]
        public void should_match_regex()
        {
            Assert.That("normal" == "normal".MustBe(Match(new Regex("(normal)"))));
        }
        
        [Test]
        public void string_should_be_in_allowed_range()
        {
            Assert.True("2" == "2".MustBe(Between(1, 3)));
        }
        
        [Test]
        public void int_should_be_in_allowed_range()
        {
            Assert.True(1 == 1.MustBe(Between<int>(1, 3)));
        }  

        [Test]
        public void should_be_equal_strings()
        {
            Assert.That("test" == "test".MustBe(Equal("test")));
        }
    }
}