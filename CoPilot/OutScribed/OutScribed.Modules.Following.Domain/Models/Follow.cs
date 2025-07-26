using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Following.Domain.Models
{
    public class Follow : AggregateRoot
    {
        public Guid ContentId { get; private set; }

        public ContentType Content { get; private set; }

        public DateTime FollowedAt { get; private set; }

        public Guid FollowerId { get; private set; }

        private Follow() { }

        private Follow(Guid contentId, ContentType content, Guid followerId)
             : base(contentId)
        {
            FollowerId = followerId;
            FollowedAt = DateTime.UtcNow;
            Content = content;
        }
        public static Follow Create(Guid contentId, ContentType content, Guid followerId)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
               new("Follower ID", followerId),
                 new("Content ID", contentId),
                   new("Content Type", content)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Follow(contentId, content, followerId);
        }
    }
}
