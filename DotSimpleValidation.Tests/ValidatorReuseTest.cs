using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Xunit;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests;

public class ValidatorReuseTest
{
    [Fact]
    public void Should_Create_Valid_Reuse_Objects()
    {
        const string validString = "aaa";
        var one = new ReuseClass(validString);
        var two = ReuseClass.Optional(validString);

        Assert.NotNull(two);
        Assert.Equal(one.Value, two!.Value);
    }

    [Fact]
    public void Should_Not_Create_Reuse_Objects()
    {
        const string invalidString = "bbb";
        
        Assert.Throws<ValidationException>(() => new ReuseClass(invalidString));
        Assert.Null(ReuseClass.Optional(invalidString));
    }

    private class ReuseClass
    {
        public readonly string Value;
        private static readonly Regex Pattern = new Regex(@"^aaa+$");

        private static readonly ImmutableList<Func<string, Result<string, string>>> Validators =
            ImmutableList.Create(OfLength(3, 3), Match(Pattern), Equal("aaa"));

        public ReuseClass(string value)
        {
            Value = value.MustBe(Validators);
        }

        private ReuseClass(string value, bool _)
        {
            Value = value;
        }

        public static ReuseClass? Optional(string? candidate)
        {
            return Validator.TryValidation(candidate, out var valid, Validators)
                ? new ReuseClass(valid, true)
                : null;
        }
    }
}