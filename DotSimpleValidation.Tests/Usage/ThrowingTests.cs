using System;
using DotSimpleValidation.Tests.Examples;
using Xunit;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests.Usage
{
    public class ThrowingFacts
    {
        [Fact]
        public void Should_Throw_For_Invalid_Guid()
        {
            Assert.Throws<ValidationException>(() =>
                "I-AM-NOT-GUID".MustBe(CreatableUsing<string, Guid>((s => new Guid(s)))));
        }

        [Fact]
        public void Should_Throw_For_Int_In_Invalid_Range()
        {
            Assert.Throws<ValidationException>(() => 3.MustBe(Between<int>(1, 2)));
        }

        [Fact]
        public void Should_Throw_For_Double_In_Invalid_Range()
        {
            Assert.Throws<ValidationException>(() => (-33.7).MustBe(Between(7.4, 11.9)));
        }

        [Fact]
        public void Should_Throw_For_Blank_String()
        {
            Assert.Throws<ValidationException>(() => " ".MustBe(NotBlankOrEmpty()));
        }

        [Fact]
        public void Should_Throw_For_Unequal_Strings()
        {
            Assert.Throws<ValidationException>(() => "Aa".MustBe(Equal("Bb")));
        }

        [Fact]
        public void Should_Throw_For_Invalid_Length()
        {
            Assert.Throws<ValidationException>(() => "Fact".MustBe(OfLength(5)));
        }

        [Fact]
        public void Should_Throw_For_Invalid_Class_Parameters()
        {
            Assert.Throws<ValidationException>(() => new ExampleClassA("okay123", 4, null));
        }
    }
}