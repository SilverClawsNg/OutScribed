namespace OutScribed.Modules.Feedback.Domain.Exceptions
{

    public class FlagAlreadyExistsException : Exception
    {
        public Guid ContentId { get; }
        public Guid FlaggerId { get; }

        public FlagAlreadyExistsException() : base("This content has already been flagged.") { }

        public FlagAlreadyExistsException(string message) : base(message) { }

        public FlagAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

        public FlagAlreadyExistsException(Guid contentId, Guid flaggerId)
            : base($"The Content '{contentId}' has already been flagged by '{flaggerId}'.")
        {
            ContentId = contentId;
            FlaggerId = flaggerId;
        }

        public FlagAlreadyExistsException(Guid contentId, Guid flaggerId, string message)
            : base(message)
        {
            ContentId = contentId;
            FlaggerId = flaggerId;
        }
    }
}
