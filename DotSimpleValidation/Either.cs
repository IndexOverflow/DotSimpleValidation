namespace DotSimpleValidation
{
    public abstract class Either<TLeft, TRight>
    {
        public static Right GoRight(TRight data)
        {
            return new Right(data);
        }

        public static Left GoLeft(TLeft error)
        {
            return new Left(error);
        }

        public abstract bool IsRight { get; }

        public class Left : Either<TLeft, TRight>
        {
            public TLeft Error { get; }
            public override bool IsRight => false;

            public Left(TLeft error)
            {
                Error = error;
            }
        }

        public class Right : Either<TLeft, TRight>
        {
            public TRight Data { get; }
            public override bool IsRight => true;

            public Right(TRight data)
            {
                Data = data;
            }
        }
    }
}