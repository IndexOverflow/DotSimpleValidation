using System;
using System.Text.RegularExpressions;

namespace DotSimpleValidation
{
    public static partial class Validators
    {
        public static Func<string, Result<string, string>> Match(Regex pattern)
        {
            return (value) =>
            {
                if (pattern.IsMatch(value))
                {
                    return Result<string, string>.MakeValid(value);
                }

                return Result<string, string>.MakeInvalid($"{value} does not match given pattern in <<caller>>");
            };
        }
        
        public static Func<string, Result<string, string>> NotBlankOrEmpty()
        {
            return value =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return Result<string, string>.MakeValid(value);
                }

                return Result<string, string>.MakeInvalid($"Given value was nullOrEmpty in <<caller>>");
            };
        }
        
        public static Func<string, Result<string, string>> Between(int min, int max)
        {
            return value =>
            {
                var parseResult = int.TryParse(value, out var intValue);

                if (parseResult && (min <= intValue && intValue <= max))
                {
                    return Result<string, string>.MakeValid(value);
                }

                return Result<string, string>.MakeInvalid($"{value} is not between allowed range {min}-{max} in <<caller>>");
            };
        }
        
        public static Func<string, Result<string, string>> OfLength(int minLength, int maxLength = 0)
        {
            return value =>
            {
                if (value.Length >= minLength)
                {
                    if (maxLength == 0 || value.Length <= maxLength)
                    {
                        return Result<string, string>.MakeValid(value);    
                    }
                }

                return Result<string, string>.MakeInvalid($"{value} did not meet length requirements in <<caller>>");
            };
        }
    }
}