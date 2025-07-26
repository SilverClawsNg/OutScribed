namespace OutScribed.Modules.Feedback.Domain.Exceptions
{

    public class FollowNotFoundException : Exception
    {

        public Guid ContentId { get; }

        public Guid FollowerId { get; }

        public FollowNotFoundException() : base("The specified follow relationship was not found.") { }

        public FollowNotFoundException(string message) : base(message) { }

        public FollowNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public FollowNotFoundException(Guid contentId, Guid followerId)
            : base($"Follow relationship for ContentId '{contentId}' and FollowerId '{followerId}' was not found.")
        {
            ContentId = contentId;
            FollowerId = followerId;
        }

        public FollowNotFoundException(Guid contentId, Guid followerId, string message)
            : base(message)
        {
            ContentId = contentId;
            FollowerId = followerId;
        }
    }

}
