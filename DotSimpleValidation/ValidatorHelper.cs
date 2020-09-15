using System;
using System.Diagnostics;
using System.Linq;

namespace DotSimpleValidation
{
    public interface INeedInvalid<TValid>
    {
        Result<TInvalid, TValid> IfInvalid<TInvalid>(TInvalid invalid);
    }
    
    public class ValidatorHelper<TValid> : INeedInvalid<TValid>
    {
        private readonly TValid _self;
        private readonly Func<TValid, Result<string, TValid>>[] _validators;

        public ValidatorHelper(TValid self, Func<TValid, Result<string, TValid>>[] validators)
        {
            _self = self;
            _validators = validators;
        }

        public Result<TInvalid, TValid> IfInvalid<TInvalid>(TInvalid invalid)
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (!_validators.Any())
            {
                throw new ValidationException($"No validators provided for ResultMustBe in {caller}");
            }

            foreach (var validator in _validators)
            {
                try
                {
                    var result = validator(_self);

                    switch (result)
                    {
                        case Result<string,TValid>.Valid _:
                            continue;
                        default:
                            return Result<TInvalid, TValid>.MakeInvalid(invalid);
                    }
                }
                catch (Exception ex)
                {
                    throw new ValidationException($"Uncaught exception occured while validating {caller}", ex);
                }
            }
            
            return (Result<TInvalid, TValid>.MakeValid(_self));
        }
    }
}