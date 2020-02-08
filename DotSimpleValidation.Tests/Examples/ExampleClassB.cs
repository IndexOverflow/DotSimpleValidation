using System.Text.RegularExpressions;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests.Examples
{
    internal class ExampleClassB
    {
        public readonly Name Name;
        public readonly Age Age;
        public readonly Occupation Occupation;

        public ExampleClassB(Name name, Age age, Occupation occupation)
        {
            Name = name;
            Age = age;
            Occupation = occupation;
        }
    }

    internal class Age
    {
        public readonly int Value;

        public Age(int? age)
        {
            Value = age.NotNull().MustBe(Between<int>(1, 150));
        }
    }

    internal class Name : DomainStringPrimitive
    {
        public Name(string? value) : base(value, @"^[a-zA-Z ]{2,50}$") // don't ever actually use this ...
        {
        }
    }
    
    internal class Occupation : DomainStringPrimitive
    {
        public Occupation(string? value) : base(value, @"^hacker$") 
        {
        }
    }

    internal abstract class DomainStringPrimitive
    {
        public readonly string Value;

        public DomainStringPrimitive(string? value, string pattern)
        {
            Value = value.NotNull().MustBe(Match(new Regex(pattern)));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}