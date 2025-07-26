namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
    public class ResendTooSoonException : OnboardingException
    {
        public string EmailAddress { get; }
        public TimeSpan TimeRemaining { get; }
        public Ulid? TempUserId { get; } // Nullable if the tempUser might not be found

        public ResendTooSoonException(string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base($"Resend attempted too soon for {emailAddress}. Please wait {timeRemaining.Minutes:F0} minutes before trying again.")
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        public ResendTooSoonException(string message, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        // You can also add constructors for inner exceptions if needed
        public ResendTooSoonException(string message, Exception innerException, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message, innerException)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }
    }
}
