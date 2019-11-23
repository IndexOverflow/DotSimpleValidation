using System.Diagnostics;

namespace DotSimpleValidation
{
    public static class NullHelpers
    {
        public static T NotNull<T>(this T? self) where T : class
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (self == null)
            {
                throw new ValidationException("Given value was null in " + caller);
            }

            return self;
        }
        
        public static T NotNull<T>(this T? self) where T : struct
        {
            var caller = new StackFrame(1)?.GetMethod()?.DeclaringType?.FullName ?? "?";

            if (self == null)
            {
                throw new ValidationException("Given value was null in " + caller);
            }

            return self.Value;
        }   
    }
}