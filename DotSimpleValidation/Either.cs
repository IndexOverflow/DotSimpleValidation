namespace DotSimpleValidation
{
    public abstract class Either<TInvalid, TValid>
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

        public class Invalid : Either<TInvalid, TValid>
        {
            public TInvalid Error { get; }
            public override bool IsValid => false;

            public Invalid(TInvalid error)
            {
                Error = error;
            }
        }

        public class Valid : Either<TInvalid, TValid>
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