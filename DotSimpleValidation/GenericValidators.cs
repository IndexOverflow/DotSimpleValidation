using System;

namespace DotSimpleValidation
{
    public static partial class Validators
    {
        public static Func<T, Result<string, T>> TrueFor<T>(Predicate<T> predicate)
        {
            return (value) =>
            {
                if (predicate(value))
                {
                    return Result<string, T>.MakeValid(value);
                }

                return Result<string, T>.MakeInvalid
                (
                    $"\"{value}\" was not true for given predicate in <<caller>>"
                );
            };
        }

        public static Func<T, Result<string, T>> Equal<T>(T match)
        {
            return (value) =>
            {
                if (Equals(value, match))
                {
                    return Result<string, T>.MakeValid(value);
                }

                return Result<string, T>.MakeInvalid
                (
                    $"\"{value}\" does not match \"{match}\" in <<caller>>"
                );
            };
        }

        public static Func<T, Result<string, T>> CreatableUsing<T, TCreatable>(Func<T, TCreatable> constructor)
        {
            return (value) =>
            {
                try
                {
                    constructor(value);
                    return Result<string, T>.MakeValid(value);
                }
                catch (Exception e)
                {
                    return Result<string, T>.MakeInvalid
                    (
                        $"{value} could not be created using given constructor in <<caller>>," +
                        $" produced ￿\"${e.Message}\""
                    );
                }
            };
        }

        public static Func<T, Result<string, T>> Between<T>(T min, T max) where T : IComparable
        {
            return value =>
            {
                var minComp = value.CompareTo(min);
                var maxComp = value.CompareTo(max);

                if (minComp >= 0 && maxComp <= 0)
                {
                    return Result<string, T>.MakeValid(value);
                }

                return Result<string, T>.MakeInvalid
                (
                    $"{value} is not between allowed range {min}-{max} in <<caller>>"
                );
            };
        }
    }
}