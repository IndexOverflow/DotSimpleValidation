using System;
using NUnit.Framework;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    public class ThrowingTests
    {
        [Test]
        public void should_throw_for_invalid_guid()
        {
            Assert.Throws<ValidationException>(() => "I-AM-NOT-GUID".MustBe(CreatableUsing<string,Guid>((s => new Guid(s)))));
        }

        [Test]
        public void should_throw_for_int_in_invalid_range()
        {
            Assert.Throws<ValidationException>(() => 3.MustBe(Between<int>(1, 2)));
        }
        
        [Test]
        public void should_throw_for_double_in_invalid_range()
        {
            Assert.Throws<ValidationException>(() => (-33.7).MustBe(Between(7.4,11.9)));
        }   

        [Test]
        public void should_throw_for_blank_string()
        {
            Assert.Throws<ValidationException>(() => " ".MustBe(NotBlankOrEmpty()));
        }

        [Test]
        public void should_throw_for_unequal_strings()
        {
            Assert.Throws<ValidationException>(() => "Aa".MustBe(Equal("Bb")));
        }
        
        [Test]
        public void should_throw_for_invalid_length()
        {
            Assert.Throws<ValidationException>(() => "test".MustBe(OfLength(5)));
        }        
        
        [Test]
        public void should_throw_for_invalid_class_parameters()
        {
            Assert.Throws<ValidationException>(() => new TestClass("okay123", 4, null));
        }
    }
}