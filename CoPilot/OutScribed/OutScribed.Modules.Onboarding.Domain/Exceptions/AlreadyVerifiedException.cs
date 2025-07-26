namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
    public class AlreadyVerifiedException : Exception
    {
        public Ulid Id { get; }

        public string EmailAddress { get; } = string.Empty;

        public AlreadyVerifiedException() : base("Account has already been verified.") { }

        public AlreadyVerifiedException(string message) : base(message) { }

        public AlreadyVerifiedException(string message, Exception innerException) : base(message, innerException) { }

        public AlreadyVerifiedException(Ulid id, string emailAddress)
            : base($"The account with ID '{id}' and email address '{emailAddress}' has already been verified.")
        {
            Id = id;
            EmailAddress = emailAddress;
        }

        public AlreadyVerifiedException(Ulid id, string emailAddress, string message)
            : base(message)
        {
            Id = id;
            EmailAddress=emailAddress;
        }
    }
}
