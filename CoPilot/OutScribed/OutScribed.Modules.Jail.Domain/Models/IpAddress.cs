using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Jail.Domain.Models
{
    public class IpAddress : AggregateRoot
    {
     
        public string Value { get; private set; } // The actual IP address string

        public DateTime? CurrentJailReleaseTime { get; private set; }

        public bool IsPermanentlyJailed { get; private set; }

        private readonly List<JailHistory> _jailHistories = [];
        public IReadOnlyList<JailHistory> JailHistories => _jailHistories.AsReadOnly();

        // Configuration for jailing rules (these are now for subsequent violations)
        private const double BASE_JAIL_MINUTES_SUBSEQUENT_VIOLATION = 10; // For 2nd, 3rd, 4th, etc.
        private const double ESCALATION_FACTOR = 1.5; // Factor by which the duration increases

        // Define the initial jail duration specifically for the first violation
        private const double INITIAL_FIRST_VIOLATION_JAIL_MINUTES = 10; // e.g., 10 minutes for the very first one

        private const int PERMANENT_BAN_THRESHOLD = 5;
        private static readonly TimeSpan VIOLATION_COUNT_WINDOW = TimeSpan.FromHours(24);


        private IpAddress() { }

        private IpAddress(Ulid id, string ipAddressValue, DateTime violationTime, 
            JailHistory history) : base(id)
        {
            Value = ipAddressValue;
            IsPermanentlyJailed = false;
            _jailHistories = [history];
            CurrentJailReleaseTime = violationTime.Add(TimeSpan.FromMinutes(INITIAL_FIRST_VIOLATION_JAIL_MINUTES));
        }

        /// <summary>
        /// Factory method to create a NEW IpAddress Aggregate Root and process its very first violation.
        /// This method implicitly handles the "first history" creation.
        /// </summary>
        public static IpAddress Create(Ulid id, string value, JailReason reason, DateTime violationTime)
        {
        
            return new IpAddress(id, value, violationTime, JailHistory.Record(reason, violationTime));
       
        }

        /// <summary>
        /// Handles a violation for an EXISTING IpAddress Aggregate Root.
        /// This method is called for the second, third, fourth, or more violations.
        /// Returns an enum indicating the outcome. Does NOT log or publish events.
        /// </summary>
        public void HandleRepeatedViolation(JailReason reason, DateTime violationTime)
        {
            // 1. Invariant Check: If already permanently jailed, simply record for history/audit.
            if (IsPermanentlyJailed)
            {
                _jailHistories.Add(JailHistory.Record(reason, violationTime));
            }

            // 2. Add the new temporary jail record to history
            _jailHistories.Add(JailHistory.Record(reason, violationTime));

            // 3. Determine the count of relevant violations within the specified time window
            // The relevantHistoryCount now includes the just-added violation.
            var relevantHistoryCount = _jailHistories
                .Count(r => r.JailedAt >= violationTime.Subtract(VIOLATION_COUNT_WINDOW));

            // 4. Determine the jailing action based on the violation count
            if (relevantHistoryCount >= PERMANENT_BAN_THRESHOLD)
            {
                // This is the 5th (or more) violation: Permanent Ban
                IsPermanentlyJailed = true;
                CurrentJailReleaseTime = DateTime.MaxValue; // Represents a permanent lockout
            }
            else
            {
                // Apply Escalating Temporary Jail Term using the exponential formula
                // Note: relevantHistoryCount here will be 2, 3, or 4 for temporary jails,
                // as the 1st violation is handled by CreateWithFirstViolation.
                double durationInMinutes = BASE_JAIL_MINUTES_SUBSEQUENT_VIOLATION * Math.Pow(ESCALATION_FACTOR, relevantHistoryCount - 1);
                TimeSpan newJailDuration = TimeSpan.FromMinutes(durationInMinutes);

                // OPTIONAL: Cap the maximum temporary jail duration
                if (newJailDuration > TimeSpan.FromHours(24)) // Example cap
                {
                    newJailDuration = TimeSpan.FromHours(24);
                }

                // Check if IP was already jailed *before* updating its release time
                // This helps the service differentiate between a new temporary jail and an extension.

                CurrentJailReleaseTime = violationTime.Add(newJailDuration);
                IsPermanentlyJailed = false; // Ensure it's explicitly not permanent

            }
        }

        public bool IsCurrentlyJailed(DateTime checkTime)
        {
            if (IsPermanentlyJailed)
            {
                return true;
            }
            return CurrentJailReleaseTime.HasValue && CurrentJailReleaseTime.Value > checkTime;
        }

        public bool UnjailManually(DateTime unjailTime)
        {
            // Check if the IP is actually jailed before attempting to unjail.
            // This makes the method idempotent and returns a clear result.
            if (!IsPermanentlyJailed && (!CurrentJailReleaseTime.HasValue || CurrentJailReleaseTime.Value <= unjailTime))
            {
                // The IP is already in a not-jailed state.
                //return UnjailResult.NoAction_AlreadyNotJailed;
            }

            // Perform the unjailing: clear both temporary and permanent states.
            CurrentJailReleaseTime = null;
            IsPermanentlyJailed = false;

            // OPTIONAL: Record this manual unjail event in the history for auditing within the aggregate.
            // This can be beneficial for a complete audit trail of the IP's jailing lifecycle.
            // If you choose to do this, ensure IpTemporaryJailRecord can handle a 'ManualUnjail' reason.
            // _jailHistory.Add(new IpTemporaryJailRecord(Value, unjailTime, JailReason.ManualUnjail));

            return false;
        }

    }
}

