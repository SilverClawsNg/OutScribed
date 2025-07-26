namespace OutScribed.Shared.Enums
{
    public enum TaleStatuses
    {
        Created,                   // Title & category only
        Submitted,                 // Submitted for review
        Checked,                   // Reviewed by checker
        Edited,                    // Edited after check
        Published,                 // Live and visible

        ReturnedByChecker,
        ReturnedByEditor,
        ReturnedByPublisher,

        RejectedByChecker,
        RejectedByEditor,
        RejectedByPublisher,

        ResubmittedToChecker,
        ResubmittedToEditor,
        ResubmittedToPublisher
    }
}
