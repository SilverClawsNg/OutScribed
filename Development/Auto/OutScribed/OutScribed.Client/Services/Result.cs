namespace OutScribed.Client.Services
{
    public class Result<T>
    {
        public readonly T Value;
        public readonly string Error;
        public bool IsFailure;

        private Result()
        {
            IsFailure = true;
            Value = default!;
            Error = default!;
        }

        private Result(int value)
        {
            IsFailure = false;
            Value = default!;
            Error = default!;
        }

        private Result(T value)
        {
            IsFailure = false;
            Value = value;
            Error = default!;
        }

        private Result(string error)
        {
            IsFailure = true;
            Value = default!;
            Error = error;
        }


        public static implicit operator Result<T>(int value) => new(value);

        //Success
        public static implicit operator Result<T>(T value) => new(value);

        //Error
        public static implicit operator Result<T>(string error) => new(error);
    }

}
