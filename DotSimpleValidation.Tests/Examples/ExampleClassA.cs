using System.Text.RegularExpressions;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests.Examples
{
    internal class ExampleClassA
    {
        public string TextField { get; }
        public int NumberField { get; }
        public string? AnotherField { get; }

        public ExampleClassA(string alphanumeric, int aNumber, string? another)
        {
            TextField = alphanumeric.MustBe(Match(new Regex("([a-zA-Z0-9])")));
            NumberField = aNumber.MustBe(Between<int>(1, 10));
            AnotherField = another.NotNull().MustBe(NotBlankOrEmpty());
        }
    }
    
    internal class NullableTestClass
    {
        public string? NullableField;
        public string NotNullableField;

        public NullableTestClass(string? nullableField, string notNullableField)
        {
            NullableField = nullableField?.MustBe(NotBlankOrEmpty());
            NotNullableField = notNullableField.NotNull().MustBe(NotBlankOrEmpty());
        }
    }
}