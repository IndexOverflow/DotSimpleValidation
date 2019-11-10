using System.Text.RegularExpressions;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    internal class TestClass
    {
        public string TextField { get; }
        public int NumberField { get; }
        public string AnotherField { get; }

        public TestClass(string alphanumeric, int aNumber, string another)
        {
            TextField = alphanumeric.MustBe(Match(new Regex("([a-zA-Z0-9])")));
            NumberField = aNumber.MustBe(Between<int>(1, 10));
            AnotherField = another.MustBe(NotNullOrBlank());
        }
    }

    internal class NullableTestClass
    {
        public string? NullableField;

        public NullableTestClass(string nullableField)
        {
            NullableField = nullableField.MustBe(NotNullOrBlank());
        }
    }
}