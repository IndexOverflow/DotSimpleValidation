using System.Text.RegularExpressions;
using static DotSimpleValidation.Validators;

namespace DotSimpleValidation.Tests
{
    internal class TestClass
    {
        public string ValidDataString { get; }
        public int ValidNumber { get; }
        public string SortaOptional { get; }

        public TestClass(string data, int aNumber, string message)
        {
            ValidDataString = data.MustBe(Match(new Regex("([a-zA-Z0-9])")));
            ValidNumber = aNumber.MustBe(Between<int>(1, 10));
            SortaOptional = message.MustBe(NotNullOrBlank());
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