using System;

namespace DotSimpleValidation
{
    public class ValidationException : ArgumentException
    {
        public ValidationException(string s) : base(s)
        {
        }

        public ValidationException(string s, Exception e) : base(s, e)
        {
            
        }
    }
}