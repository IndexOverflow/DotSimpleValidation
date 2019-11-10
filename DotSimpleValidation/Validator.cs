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
                try
                {
                    var result = validator(self);

                    if (result is Either<string, T>.Invalid left)
                    {
                        throw new ValidationException(left.Error.Replace("<<caller>>", caller));
                    }
                }
                catch (NullReferenceException ex)
                {
                    throw new ValidationException($"Uncaught exception occured while validating {caller}", ex);
                }

            }

            return self;
        }

        /// <summary>
        /// Runs through given validators. Fails fast, first encountered error will return 
        /// </summary>
        /// <param name="self">Any Type</param>
        /// <param name="validators">Validators from <see cref="Validators"/></param>
        /// <typeparam name="TValid">The extended Type to be validated</typeparam>
        /// <returns><see cref="Either{TInvalid,TValid}"/> with Either.Left&lt;TLeft&gt; if error, Either.Right&lt;TRight&gt; if success</returns>
        public static Either<string, TValid> EitherMustBe<TValid>
        (
            this TValid self,
            params Func<TValid, Either<string, TValid>>[] validators
        )
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (!validators.Any())
            {
                throw new ArgumentException($"No validators provided for EitherMustBe in {caller}");
            }

            Either<string, TValid>? result = null;

            foreach (var validator in validators)
            {
                try
                {
                    result = validator(self);

                    if (result is Either<string, TValid>.Invalid left)
                    {
                        return Either<string, TValid>.MakeInvalid(left.Error.Replace("<<caller>>", caller));
                    }
                }
                catch (Exception ex)
                {
                    throw new ValidationException($"Uncaught exception occured while validating {caller}", ex);
                }
            }

            return result as Either<string, TValid>.Valid;
        }
    }              
}