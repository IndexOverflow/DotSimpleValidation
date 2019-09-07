using System;
using System.Text.RegularExpressions;

namespace DotSimpleValidation
{
    public static class Validators
    {
        public static Func<string, Either<string, string>> Equal(string match)
        {
            return (value) =>
            {
                if (Equals(value, match))
                {
                    return Either<string, string>.GoRight(value);
                }

                return Either<string, string>.GoLeft($"\"{value}\" does not match \"{match}\" in <<caller>>");
            };
        }

        public static Func<string, Either<string, string>> Match(Regex pattern)
        {
            return (value) =>
            {
                if (pattern.IsMatch(value))
                {
                    return Either<string, string>.GoRight(value);
                }

                return Either<string, string>.GoLeft($"{value} does not match given pattern in <<caller>>");
            };
        }

        public static Func<string, Either<string, string>> NotNullOrBlank()
        {
            return value =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return Either<string, string>.GoRight(value);
                }

                return Either<string, string>.GoLeft($"\"{value}\" is nullOrEmpty in <<caller>>");
            };
        }

        public static Func<T, Either<string, T>> Between<T>(int min, int max)
        {
            return value =>
            {
                var parseResult = true;
                var intValue = 0;

                switch (value)
                {
                    case string s:
                        parseResult = int.TryParse(s, out intValue);
                        break;
                    case int i:
                        intValue = i;
                        break;
                }

                if (parseResult && (min <= intValue && intValue <= max))
                {
                    return Either<string, T>.GoRight(value);
                }

                return Either<string, T>.GoLeft($"{value} is not between allowed range {min}-{max} in <<caller>>");
            };
        }
    }
}