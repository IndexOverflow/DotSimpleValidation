using System;
using System.Diagnostics;
using System.Linq;

namespace DotSimpleValidation
{
    public static class Validator
    {
        /// <summary>
        /// Runs through given validators. Fails fast, first error will throw <see cref="ValidationException"/>
        /// </summary>
        /// <param name="self">Any Type</param>
        /// <param name="validators">Validators from <see cref="Validators"/></param>
        /// <typeparam name="T">The extended Type to be validated</typeparam>
        /// <returns>T</returns>
        /// <exception cref="ValidationException">Thrown if a validator fails. Extends <see cref="ArgumentException"/>.</exception>
        public static T MustBe<T>(this T self, params Func<T, Either<string, T>>[] validators)
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (!validators.Any())
            {
                throw new ArgumentException($"No validators provided for MustBe in {caller}");
            }

            foreach (var validator in validators)
            {
                var result = validator(self);

                if (result is Either<string, T>.Left left)
                {
                    throw new ValidationException(left.Error.Replace("<<caller>>", caller));
                }
            }

            return self;
        }

        /// <summary>
        /// Runs through given validators. Fails fast, first encountered error will return 
        /// </summary>
        /// <param name="self">Any Type</param>
        /// <param name="validators">Validators from <see cref="Validators"/></param>
        /// <typeparam name="TRight">The extended Type to be validated</typeparam>
        /// <returns><see cref="Either{TLeft,TRight}"/> with Either.Left&lt;TLeft&gt; if error, Either.Right&lt;TRight&gt; if success</returns>
        public static Either<string, TRight> EitherMustBe<TRight>
        (
            this TRight self,
            params Func<TRight,
                Either<string, TRight>>[] validators
        )
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (!validators.Any())
            {
                throw new ArgumentException($"No validators provided for EitherMustBe in {caller}");
            }

            Either<string, TRight> result = null;

            foreach (var validator in validators)
            {
                result = validator(self);

                if (result is Either<string, TRight>.Left left)
                {
                    return Either<string, TRight>.GoLeft(left.Error.Replace("<<caller>>", caller));
                }
            }

            return result as Either<string, TRight>.Right;
        }
    }
}