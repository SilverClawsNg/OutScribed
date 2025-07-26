using OutScribed.Modules.Onboarding.Domain.Enums;
using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Onboarding.Domain.Models
{
    public class TempUser : AggregateRoot
    {

        public string EmailAddress { get; private set; } = default!;
        public string IpAddress { get; private set; } = default!;
        public OneTimeToken Token { get; private set; } = default!; // Verification token
        public DateTime LastUpdated { get; private set; } // CRITICAL: Reference for all timing rules

        public int ResendsCounter { get; private set; } // Resends within the current 10-min window
        public int VerifiedAttempts { get; private set; } // Verification attempts within the current 10-min window

        public bool IsVerified { get; private set; } // Indicates if email has been verified via token
        public bool IsLockedOut { get; private set; } // True if currently in 30-min lockout
        public DateTime? LockoutUntil { get; private set; } // When lockout expires


        private TempUser() { } // For EF Core

        private TempUser(Ulid id, string emailAddress, string ipAddress, OneTimeToken token) 
            : base(id)
        {
            EmailAddress = emailAddress;
            IpAddress = ipAddress;
            Token = token;
            LastUpdated = DateTime.UtcNow;
            ResendsCounter = 0;
            VerifiedAttempts = 0;
            IsVerified = false;
            IsLockedOut = false;
            LockoutUntil = null;
        }

        public static TempUser Create(Ulid id, string emailAddress, string ipAddress)
        {

            var invalidFields = Validator.GetInvalidFields(
                 [
                    new("Email Address", emailAddress, minLength: 3, maxLength: 255),
                    new("Ip Address", ipAddress, maxLength: 255),
                    new("TempUser ID", id)
                 ]
               );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new TempUser(id, emailAddress, ipAddress, OneTimeToken.Generate());

        }

        //Indicates if account is currently locked out. LockoutUntil can be preset to 30 miutes
        public bool IsCurrentlyLockedOut => IsLockedOut && LockoutUntil.HasValue && LockoutUntil.Value > DateTime.UtcNow;

        public TimeSpan TimeUntilLockoutEnds
        {
            get
            {
                if (IsLockedOut && LockoutUntil.HasValue)
                {
                    var remainingTime = LockoutUntil.Value - DateTime.UtcNow;
                    return remainingTime > TimeSpan.Zero ? remainingTime : TimeSpan.Zero;
                }
                return TimeSpan.Zero; // Not locked out, or LockoutUntil is null
            }
        }

        private void ApplyLockout()
        {
            IsLockedOut = true;
            LockoutUntil = DateTime.UtcNow.AddMinutes(30);
            LastUpdated = DateTime.UtcNow; // LastUpdated becomes the start of the lockout period
        }

        public void UpdateEmailAddress(string emailAddress)
        {
            var invalidFields = Validator.GetInvalidFields(
            [
                   new("Email Address", emailAddress, minLength: 3, maxLength: 255)
                ]
              );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            // You might add additional domain rules here, e.g.:
            // - If EmailAddress is already IsVerified, maybe disallow change, or require re-verification?
            //   (Based on your rule, if it's verified, it will eventually be cleaned up anyway or moved to main user)
            // - Is the new email format valid? (Ideally validated in DTO/request)

            EmailAddress = emailAddress;
            // LastUpdated is NOT updated here, as this is a data change, not an "activity" for rate limiting.
            // It will be updated when HandleResendRequest is called subsequently.
        }

        private void ResetStateIfEligible()
        {
            // If was locked out but now expired
            if (IsLockedOut && !IsCurrentlyLockedOut)
            {
                ResendsCounter = 0;
                VerifiedAttempts = 0;
                IsLockedOut = false;
                LockoutUntil = null;
                LastUpdated = DateTime.UtcNow; // New clean slate starts now
            }
            // If not locked out, but the 10-minute activity window has passed.
            // IMPORTANT: This uses the existing LastUpdated to check the window expiry.
            else if (!IsLockedOut && (DateTime.UtcNow - LastUpdated).TotalMinutes >= 10)
            {
                ResendsCounter = 0;
                VerifiedAttempts = 0;
                LastUpdated = DateTime.UtcNow; // New 10-minute window starts now
            }
            // If currently locked out OR within the 10-minute window, LastUpdated and counters remain as they are.
        }

        public DomainResults HandleResendRequest(string? emailAddress = null) // Made optional
        {
            // 1. Prepare the state for the current request
            ResetStateIfEligible();

            // 2. Process optional email address update if provided and different
            if (emailAddress != null)
            {
                // You might add an invariant here: e.g., if (IsVerified) throw InvalidOperationException("Cannot change email for a verified temp user.");
                UpdateEmailAddress(emailAddress);
                // Note: LastUpdated, ResendsCounter, etc., will be handled by the subsequent logic or ResetStateIfEligible()
            }

            // 3. Rule 1: 60-second delay between resends
            if ((DateTime.UtcNow - LastUpdated).TotalSeconds < 60)
            {
                ApplyLockout();
                return DomainResults.TooSoon;
            }

            // 4. Increment counter
            ResendsCounter++;

            // 5. Rule 2: Max 5 resends within 10 minutes
            if (ResendsCounter > 5)
            {
                ApplyLockout();
                return DomainResults.TooManyResends;
            }

            // If we reach here, no violation occurred. Generate new token.
            Token = OneTimeToken.Generate();

            IsVerified = false;

            return DomainResults.Success;
        }

        public DomainResults HandleVerificationAttempt(string providedToken)
        {
            // 1. Prepare the state for the current request
            ResetStateIfEligible();

            // 2. Increment counter
            VerifiedAttempts++;
            // IMPORTANT: DO NOT UPDATE LastUpdated = DateTime.UtcNow HERE.

            // 3. Rule 3: Max 5 verification attempts within 10 minutes
            // This check now correctly applies to the window defined by the LastUpdated anchor.
            if (VerifiedAttempts > 5)
            {
                ApplyLockout();
                return DomainResults.TooManyVerificationAttempts;
            }

            // 4. Check token validity
            if (Token.Token != providedToken || DateTime.UtcNow > Token.ExpiresAt)
            {
                return DomainResults.InvalidTokenOrExpired;
            }

            IsVerified = true;
            return DomainResults.Success;
        }

        public void ResendToken(string emailAddress)
        {

            var invalidFields = Validator.GetInvalidFields(
                 [
                    new("Email Address", emailAddress, minLength: 3, maxLength: 255)
                 ]
               );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            EmailAddress = emailAddress;

            Token = OneTimeToken.Generate();
            ResendsCounter++;
            LastUpdated = DateTime.UtcNow;
        }

    }

}


