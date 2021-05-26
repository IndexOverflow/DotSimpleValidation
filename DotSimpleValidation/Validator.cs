using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        /// <exception cref="ValidationException">Thrown if a validator fails. Extends <see cref="ArgumentException"/>.
        /// </exception>
        public static T MustBe<T>(this T self, params Func<T, Result<string, T>>[] validators)
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (!validators.Any())
            {
                throw new ValidationException($"No validators provided for MustBe in {caller}");
            }

            foreach (var validator in validators)
            {
                try
                {
                    var result = validator(self);

                    if (result is Result<string, T>.Invalid invalid)
                    {
                        throw new ValidationException(invalid.Error.Replace("<<caller>>", caller));
                    }
                }
                catch (NullReferenceException ex)
                {
                    throw new ValidationException($"Unexpected NullReferenceException occured while validating {caller}", ex);
                }
            }

            return self;
        }
        
        /// <summary>
        /// Will try to validate the given struct T
        /// against provided validators. 
        /// </summary>
        /// <param name="value">The value to be validated</param>
        /// <param name="valid">Validated struct of T if valid, otherwise default(T)</param>
        /// <param name="validators">Validators from <see cref="Validators"/></param>
        /// <typeparam name="T">The extended struct to be validated</typeparam>
        /// <returns>true if all validators passed, false otherwise</returns>
        /// <exception cref="ValidationException"></exception>
        public static bool TryValidation<T>
        (
            [AllowNull] T value,
            [NotNullWhen(true)] out T valid,
            params Func<T, Result<string, T>>[] validators
        )  
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (validators == null || !validators.Any())
            {
                throw new ValidationException($"No validators provided for TryValidation in {caller}");
            }

            if (TryValidate(value,validators!))
            {
                valid = value!;
                return true;
            }

            valid = default!;
            return false;
        }

        private static bool TryValidate<T>
        (
            [AllowNull] T value,
            params Func<T, Result<string, T>>[] validators
        )
        {
            if (value == null)
            {
                return false;
            }
            
            foreach (var validator in validators)
            {
                try
                {
                    var result = validator(value);

                    if (result is Result<string, T>.Invalid _)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            
            return true;
        } 

        /// <summary>
        /// Runs through given validators. Must be followed by <see cref="IfInvalid(TInvalid invalid)"/>"/>"/>
        /// Fails fast, first encountered error will return 
        /// </summary>
        /// <param name="self">Any Type</param>
        /// <param name="validators">Validators from <see cref="Validators"/></param>
        /// <typeparam name="T">The extended Type to be validated</typeparam>
        /// <returns><see cref="Result{TInvalid,TValid}"/></returns>
        public static INeedInvalid<T> IsValid<T>
        (
            this T self,
            params Func<T, Result<string, T>>[] validators
        )
        {
            return new ValidatorHelper<T>(self, validators);
        }
    }


}