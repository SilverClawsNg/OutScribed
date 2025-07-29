namespace OutScribed.Modules.Identity.Domain.Enums
{
    public enum DomainResults
    {
        Success, // Request was processed successfully
        TooSoon, // Violation: Resend attempted before 60 seconds (results in lockout)
        TooManyResends, // Violation: More than 5 resends within 10 minutes (results in lockout)
        TooManyVerificationAttempts, // Violation: More than 5 verification attempts within 10 minutes (results in lockout)
        InvalidTokenOrExpired, // Verification failed due to bad token/expiry (does NOT result in lockout)
        CurrentlyLockedOut // Attempted action while already in a lockout period
    }

}
