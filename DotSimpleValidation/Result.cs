namespace DotSimpleValidation
{
    public abstract class Result<TInvalid, TValid>
    {
        public static Valid MakeValid(TValid data)
        {
            return new Valid(data);
        }

        public static Invalid MakeInvalid(TInvalid error)
        {
            return new Invalid(error);
        }

        public abstract bool IsValid { get; }

        public class Invalid : Result<TInvalid, TValid>
        {
            public TInvalid Error { get; }
            public override bool IsValid => false;

            public Invalid(TInvalid error)
            {
                Error = error;
            }
        }

        public class Valid : Result<TInvalid, TValid>
        {
            public TValid Data { get; }
            public override bool IsValid => true;

            public Valid(TValid data)
            {
                Data = data;
            }
        }
    }
}