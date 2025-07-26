namespace Backend.Domain.Exceptions
{

    public class Result<T>
    {
        public readonly T Value;
        public readonly Error Error;
        public bool IsFailure;

        private Result()
        {
            IsFailure = true;
            Value = default!;
            Error = default!;
        }

        private Result(T value)
        {
            IsFailure = false;
            Value = value;
            Error = default!;
        }

        private Result(Error error)
        {
            IsFailure = true;
            Value = default!;
            Error = error;
        }

        //Success
        public static implicit operator Result<T>(T value) => new(value);

        //Error
        public static implicit operator Result<T>(Error error) => new(error);

    }
}
