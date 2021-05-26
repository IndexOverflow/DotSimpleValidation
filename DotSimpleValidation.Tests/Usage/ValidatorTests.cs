using System;
using System.Text.RegularExpressions;
using DotSimpleValidation.Tests.Examples;
using Xunit;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests.Usage
{
    public class ValidatorTests
    {
        [Fact]
        public void Should_Be_True_For_Predicate()
        {
            Assert.Equal("true", "true".MustBe(TrueFor<string>((s) => s.Contains("tru"))));
        }

        [Fact]
        public void Should_Create_Uri_With_Creatable_Using()
        {
            Assert.Equal
            (
                "https://github.com",
                "https://github.com".MustBe(CreatableUsing<string, Uri>(s => new Uri(s))).ToString()
            );
        }

        [Fact]
        public void Should_Match_Regex()
        {
            Assert.Equal("normal", "normal".MustBe(Match(new Regex("(normal)"))));
        }

        [Fact]
        public void Should_See_String_In_Allowed_Range()
        {
            Assert.Equal("2", "2".MustBe(Between(1, 3)));
        }

        [Fact]
        public void Should_See_Int_In_Allowed_Range()
        {
            Assert.True(1 == 1.MustBe(Between<int>(1, 3)));
        }

        [Fact]
        public void Should_See_Equal_Strings()
        {
            Assert.Equal("test", "test".MustBe(Equal("test")));
        }
    }
}