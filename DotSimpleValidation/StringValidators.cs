using System;
using System.Text.RegularExpressions;

namespace DotSimpleValidation
{
    public static partial class Validators
    {
        public static Func<string, Either<string, string>> Match(Regex pattern)
        {
            return (value) =>
            {
                if (pattern.IsMatch(value))
                {
                    return Either<string, string>.MakeValid(value);
                }

                return Either<string, string>.MakeInvalid($"{value} does not match given pattern in <<caller>>");
            };
        }
        
        public static Func<string, Either<string, string>> NotNullOrBlank()
        {
            return value =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return Either<string, string>.MakeValid(value);
                }

                return Either<string, string>.MakeInvalid($"Given value was nullOrEmpty in <<caller>>");
            };
        }
        
        public static Func<string, Either<string, string>> Between(int min, int max)
        {
            return value =>
            {
                var parseResult = true;
                var intValue = 0;

                parseResult = int.TryParse(value, out intValue);

                if (parseResult && (min <= intValue && intValue <= max))
                {
                    return Either<string, string>.MakeValid(value);
                }

                return Either<string, string>.MakeInvalid($"{value} is not between allowed range {min}-{max} in <<caller>>");
            };
        }
    }
}