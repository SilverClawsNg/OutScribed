using OutScribed.Modules.Identity.Domain.Enums;
using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Domain.Models
{

    public class Account : AggregateRoot
    {

        public DateTime RegisteredAt { get; private set; }
        public string EmailAddress { get; private set; } = default!;
        public string Username { get; set; } = default!;
        public Password Password { get; private set; } = default!;
        public OneTimeToken? Token { get; private set; }
        public RefreshToken? RefreshToken { get; private set; }
        public Profile? Profile { get; private set; }
        public Writer? Writer { get; private set; }
        public Admin? Admin { get; private set; }
        public bool DoNotResendOtp { get; init; }

        public bool IsLockedOut { get; private set; } // True if currently in 30-min lockout
        public DateTime? LockoutUntil { get; private set; } // When lockout expires
        public int ResendsCounter { get; private set; } // Resends within the current 10-min window
        public DateTime LastUpdated { get; private set; } // CRITICAL: Reference for all timing rules


        private readonly List<Contact> _contacts = [];
        public IReadOnlyList<Contact> Contacts => [.. _contacts];

        private readonly List<LoginHistory> _loginHistories = [];
        public IReadOnlyList<LoginHistory> LoginHistories => [.. _loginHistories];

        private readonly List<Notification> _notifications = [];
        public IReadOnlyList<Notification> Notifications => [.. _notifications];

        private readonly List<Follow> _follows = [];
        public IReadOnlyCollection<Follow> Follows => _follows.AsReadOnly();

        private Account() { }

        private Account(Ulid id, string emailAddress, string username, Password password,
           Profile profile, Notification notification)
           : base(id)
        {
            EmailAddress = emailAddress;
            Username = username;
            Password = password;
            Profile = profile;
            RegisteredAt = DateTime.UtcNow;
            DoNotResendOtp = false;
            _contacts = [];
            _loginHistories = [];
            _notifications = [notification];
        }

        public static Account Create(Ulid id, string emailAddress, string username,
          string password)
        {

            var invalidFields = Validator.GetInvalidFields(
              [
                  new("Email Address", emailAddress, minLength: 3, maxLength: 255),
                  new("Username", username, minLength: 3, maxLength: 20),
                  new("Password", password, minLength: 8),
                   new("Account ID", id)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //Create a notification
          
            return new Account(
                id,
                emailAddress, 
                username, 
                Password.Create(password), 
                Profile.Create(),
                Notification.Create("Account Created", NotificationType.Account_Created, 
                ContentType.Account)
                );

        }

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

        private void ResetStateIfEligible()
        {
            // If was locked out but now expired
            if (IsLockedOut && !IsCurrentlyLockedOut)
            {
                ResendsCounter = 0;
                IsLockedOut = false;
                LockoutUntil = null;
                LastUpdated = DateTime.UtcNow; // New clean slate starts now
            }
            // If not locked out, but the 10-minute activity window has passed.
            // IMPORTANT: This uses the existing LastUpdated to check the window expiry.
            else if (!IsLockedOut && (DateTime.UtcNow - LastUpdated).TotalMinutes >= 10)
            {
                ResendsCounter = 0;
                LastUpdated = DateTime.UtcNow; // New 10-minute window starts now
            }
            // If currently locked out OR within the 10-minute window, LastUpdated and counters remain as they are.
        }

        public DomainResults HandleResendRequest() // Made optional
        {
            // 1. Prepare the state for the current request
            ResetStateIfEligible();

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

            return DomainResults.Success;
        }

    }
}
