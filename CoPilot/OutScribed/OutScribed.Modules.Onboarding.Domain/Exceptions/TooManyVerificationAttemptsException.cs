using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
  
    public class TooManyVerificationAttemptsException : OnboardingException
    {
        public string EmailAddress { get; }
        public TimeSpan TimeRemaining { get; }
        public Ulid? TempUserId { get; } // Nullable if the tempUser might not be found

        public TooManyVerificationAttemptsException(string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base($"Too many verification attempts out for {emailAddress}. Please wait {timeRemaining.Minutes:F0} minutes before trying again.")
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        public TooManyVerificationAttemptsException(string message, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        // You can also add constructors for inner exceptions if needed
        public TooManyVerificationAttemptsException(string message, Exception innerException, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message, innerException)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }
    }

}
