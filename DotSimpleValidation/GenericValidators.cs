using System;

namespace DotSimpleValidation
{
    public static partial class Validators
    {
        public static Func<T, Either<string,T>> BeTrue<T>(Predicate<T> predicate)
        {
            return (value) =>
            {
                if (predicate(value))
                {
                    return Either<string, T>.MakeValid(value);
                }

                return Either<string, T>.MakeInvalid($"\"{value}\" was not true for given predicate in <<caller>>");
            };
        }
        
        public static Func<T, Either<string, T>> Equal<T>(T match)
        {
            return (value) =>
            {
                if (Equals(value, match))
                {
                    return Either<string, T>.MakeValid(value);
                }

                return Either<string, T>.MakeInvalid($"\"{value}\" does not match \"{match}\" in <<caller>>");
            };
        }
        
        public static Func<T, Either<string, T>> CreatableUsing<T,TCreatable>(Func<T, TCreatable> constructor)
        {
            return (value) =>
            {

                try
                {
                    constructor(value);
                    return Either<string, T>.MakeValid(value);
                }
                catch (Exception e)
                {
                    return Either<string, T>.MakeInvalid($"{value} could not be created using given constructor in <<caller>>, produced ￿\"${e.Message}\"");
                }

            };
        }
        
        public static Func<T, Either<string, T>> Between<T>(T min, T max) where T : IComparable 
        {
            return value =>
            {
                var minComp = value.CompareTo(min);
                var maxComp = value.CompareTo(max);

                if (minComp >= 0 && maxComp <= 0)
                {
                    return Either<string, T>.MakeValid(value);
                }

                return Either<string, T>.MakeInvalid($"{value} is not between allowed range {min}-{max} in <<caller>>");
            };
        }        
    }
}