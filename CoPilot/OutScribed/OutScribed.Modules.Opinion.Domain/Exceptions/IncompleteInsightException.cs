namespace OutScribed.Modules.Analysis.Domain.Exceptions
{
   
    public class IncompleteInsightException : Exception
    {

        public Ulid InsightId { get; }

        public IncompleteInsightException() : base("Incomplete insights are ineligible for published.") { }

        public IncompleteInsightException(string message) : base(message) { }

        public IncompleteInsightException(string message, Exception innerException) : base(message, innerException) { }

        public IncompleteInsightException(Guid insightId)
            : base($"The Insight '{insightId}' is incomplete and ineligible for publication.")
        {
            InsightId = insightId;
        }

        public IncompleteInsightException(Guid insightId, string message)
            : base(message)
        {
            InsightId = insightId;
        }
    }

}
