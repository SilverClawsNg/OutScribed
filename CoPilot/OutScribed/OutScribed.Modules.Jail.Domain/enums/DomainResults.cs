namespace OutScribed.Modules.Jail.Domain.enums
{
 
    public enum DomainResults
    {
        /// <summary>
        /// The IP was permanently banned due to accumulating too many temporary violations.
        /// </summary>
        PermanentlyBanned,

        /// <summary>
        /// The IP was temporarily jailed for a new duration.
        /// </summary>
        TemporarilyJailed,

        /// <summary>
        /// A violation was recorded, but the IP was already temporarily jailed,
        /// and its current jail duration was either extended or remained the same.
        /// </summary>
        ViolationRecordedAndJailExtended, // Or just ViolationRecordedIfAlreadyJailed

        /// <summary>
        /// A violation was recorded, but the IP was already permanently banned.
        /// No change to jailing status occurred.
        /// </summary>
        ViolationRecordedAlreadyPermanentlyBanned,

        // You could add other states like:
        // NoAction_InvalidIpAddress, (if validation moves here)
        // NoAction_IgnoredViolation,
    }
}
