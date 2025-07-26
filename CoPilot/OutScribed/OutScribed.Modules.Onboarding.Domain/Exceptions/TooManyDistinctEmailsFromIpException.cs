using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
    public class TooManyDistinctEmailsFromIpException : OnboardingException
    {
        public string IpAddress { get; }
        public int MaxDistinctEmailsAllowed { get; }
        public TimeSpan CheckWindow { get; }
        public string AttemptedEmailAddress { get; }

        public TooManyDistinctEmailsFromIpException(
            string ipAddress,
            int maxDistinctEmailsAllowed,
            TimeSpan checkWindow,
            string attemptedEmailAddress)
            : base($"Too many distinct email token requests ({maxDistinctEmailsAllowed} allowed) from IP address '{ipAddress}' within {checkWindow.TotalMinutes:F0} minutes for email '{attemptedEmailAddress}'.")
        {
            IpAddress = ipAddress;
            MaxDistinctEmailsAllowed = maxDistinctEmailsAllowed;
            CheckWindow = checkWindow;
            AttemptedEmailAddress = attemptedEmailAddress;
        }
    }
}
