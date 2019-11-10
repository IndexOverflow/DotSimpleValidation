using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ValidatorTests
    {
        #pragma warning disable CS8625
        [Test]
        public void should_fail_on_null()
        {
            Assert.Throws<ValidationException>(() => new NullableTestClass(null));
        }
        #pragma warning restore CSCS8625
        
        [Test]
        public void should_be_true_for_predicate()
        {
            Assert.That("is a test" == "is a test".MustBe(BeTrue<string>((s) => s.Contains("test"))));
        }

        [Test]
        public void should_create_uri_with_creatable_using()
        {
            Assert.That("https://github.com" == "https://github.com".MustBe(CreatableUsing<string,Uri>(s => new Uri(s))));
        }
        
        [Test]
        public void should_fail_with_creatable_using()
        {
            Assert.Throws<ValidationException>(() => "I-AM-NOT-GUID".MustBe(CreatableUsing<string,Guid>((s => new Guid(s)))));
        }
        
        [Test]
        public void should_match_regex()
        {
            Assert.That("normal" == "normal".MustBe(Match(new Regex("(normal)"))));
        }

        [Test]
        public void should_not_in_allowed_range()
        {
            Assert.Throws<ValidationException>(() => 3.MustBe(Between<int>(1, 2)));
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
        public void double_should_not_be_in_allowed_range()
        {
            Assert.Throws<ValidationException>(() => (-33.7).MustBe(Between(7.4,11.9)));
        }   

        [Test]
        public void should_not_be_blank()
        {
            Assert.Throws<ValidationException>(() => " ".MustBe(NotNullOrBlank()));
        }

        [Test]
        public void should_be_not_be_equal_strings()
        {
            Assert.Throws<ValidationException>(() => "Aa".MustBe(Equal("Bb")));
        }
        
        [Test]
        public void should_be_be_equal_strings()
        {
            Assert.That("test" == "test".MustBe(Equal("test")));
        }        

        #pragma warning disable CS8625
        [Test]
        public void should_not_create_test_class()
        {
            Assert.Throws<ValidationException>(() => new TestClass("okay123", 4, null));
        }
        #pragma warning restore CS8625
    }
}