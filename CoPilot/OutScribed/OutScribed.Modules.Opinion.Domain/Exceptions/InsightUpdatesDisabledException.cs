namespace OutScribed.Modules.Analysis.Domain.Exceptions
{
   
    public class InsightUpdatesDisabledException : Exception
    {

        public Ulid InsightId { get; }

        public string UpdateType { get; } = string.Empty;

        public InsightUpdatesDisabledException() : base("Post publication updates has been disabled on this insight.") { }

        public InsightUpdatesDisabledException(string message) : base(message) { }

        public InsightUpdatesDisabledException(string message, Exception innerException) : base(message, innerException) { }

        public InsightUpdatesDisabledException(Ulid insightId, string updateType)
            : base($"The post publication update '{updateType}' has been disabled on the Insight '{insightId}'.")
        {
            InsightId = insightId;
            UpdateType = updateType;
        }

        public InsightUpdatesDisabledException(Ulid insightId, string updateType, string message)
            : base(message)
        {
            InsightId = insightId;
            UpdateType = updateType;
        }
    }

}
