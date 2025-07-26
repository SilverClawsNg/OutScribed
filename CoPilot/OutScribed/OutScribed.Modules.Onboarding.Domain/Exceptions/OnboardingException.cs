namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
    public class OnboardingException : Exception
    {
        public OnboardingException() { }
        public OnboardingException(string message) : base(message) { }
        public OnboardingException(string message, Exception innerException) : base(message, innerException) { }
        // Add custom properties like ErrorCode, or correlation ID if needed across all exceptions
    }
}
