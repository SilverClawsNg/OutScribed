namespace OutScribed.Modules.Feedback.Domain.Exceptions
{

    public class RateAlreadyExistsException : Exception
    {

        public Guid ContentId { get; }

        public Guid RaterId { get; }

        public RateAlreadyExistsException() : base("This content has already been rated.") { }

        public RateAlreadyExistsException(string message) : base(message) { }

        public RateAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

        public RateAlreadyExistsException(Guid contentId, Guid raterId)
            : base($"The Content '{contentId}' has already been rated by '{raterId}'.")
        {
            ContentId = contentId;
            RaterId = raterId;
        }

        public RateAlreadyExistsException(Guid contentId, Guid raterId, string message)
            : base(message)
        {
            ContentId = contentId;
            RaterId = raterId;
        }

    }
}
