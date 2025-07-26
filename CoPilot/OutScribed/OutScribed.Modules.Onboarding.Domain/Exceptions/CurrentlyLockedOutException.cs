using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
  
    public class CurrentlyLockedOutException : OnboardingException
    {
        public string EmailAddress { get; }
        public TimeSpan TimeRemaining { get; }
        public Ulid? TempUserId { get; } // Nullable if the tempUser might not be found

        public CurrentlyLockedOutException(string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base($"Account currently locked out for {emailAddress}. Please wait {timeRemaining.Minutes:F0} minutes.")
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        public CurrentlyLockedOutException(string message, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        // You can also add constructors for inner exceptions if needed
        public CurrentlyLockedOutException(string message, Exception innerException, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message, innerException)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }
    }

}
