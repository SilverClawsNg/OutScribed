namespace OutScribed.Modules.Feedback.Domain.Exceptions
{

    public class FollowAlreadyExistsException : Exception
    {
        public Guid ContentId { get; }
        public Guid FollowerId { get; }

        public FollowAlreadyExistsException() : base("The specified follow relationship already exists.") { }

        public FollowAlreadyExistsException(string message) : base(message) { }

        public FollowAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

        public FollowAlreadyExistsException(Guid contentId, Guid followerId)
            : base($"Follow relationship for ContentId '{contentId}' and FollowerId '{followerId}' already exists.")
        {
            ContentId = contentId;
            FollowerId = followerId;
        }

        public FollowAlreadyExistsException(Guid contentId, Guid followerId, string message)
            : base(message)
        {
            ContentId = contentId;
            FollowerId = followerId;
        }
    }
}
