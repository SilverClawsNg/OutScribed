using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Analysis.Domain.Models
{
    public class Follow : Entity
    {

        public DateTime FollowedAt { get; private set; }

        public Ulid FollowerId { get; private set; }


        public Ulid InsightId { get; private set; } = default!;

        public Insight Insight { get; private set; } = default!;

        private Follow() { }

        private Follow(Ulid id, Ulid followerId)
             : base(id)
        {
            FollowerId = followerId;
            FollowedAt = DateTime.UtcNow;
        }
        public static Follow Create(Ulid id, Ulid followerId)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
               new("User ID", followerId),
                 new("Follow ID", id)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Follow(id, followerId);
        }
    }
}
