using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public static Func<string, Result<string, string>> Match(IEnumerable<char> pattern)
        {
            return (value) =>
            {
                if (value.Any(v => !pattern.Contains(v)))
                {
                    return Result<string, string>.MakeInvalid($"{value} does not match given pattern in <<caller>>");
                }
                
                return Result<string, string>.MakeValid(value);
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

                if (parseResult)
                {
                    var minAbs = Math.Abs(min);
                    var maxAbs = Math.Abs(max);
                    var valAbs = Math.Abs(intValue);
                    
                        if (minAbs <= valAbs && valAbs <= maxAbs)
                        {
                            return Result<string, string>.MakeValid(value);        
                        }
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