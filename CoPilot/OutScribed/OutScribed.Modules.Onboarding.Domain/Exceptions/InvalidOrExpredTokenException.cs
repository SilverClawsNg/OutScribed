using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
  
    public class InvalidOrExpredTokenException : OnboardingException
    {
        public string EmailAddress { get; }
        public TimeSpan TimeRemaining { get; }
        public Ulid? TempUserId { get; } // Nullable if the tempUser might not be found

        public InvalidOrExpredTokenException(string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base($"Verification failed for {emailAddress}. Try again.")
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        public InvalidOrExpredTokenException(string message, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }

        // You can also add constructors for inner exceptions if needed
        public InvalidOrExpredTokenException(string message, Exception innerException, string emailAddress, TimeSpan timeRemaining, Ulid? tempUserId = null)
            : base(message, innerException)
        {
            EmailAddress = emailAddress;
            TimeRemaining = timeRemaining;
            TempUserId = tempUserId;
        }
    }

}
