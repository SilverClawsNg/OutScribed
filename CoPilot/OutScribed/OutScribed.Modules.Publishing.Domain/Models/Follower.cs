using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Publishing.Domain.Models
{
    public class Follower : Entity
    {
    
        public DateTime FollowedAt { get; private set; }

        public Ulid FollowerId { get; private set; }


        public Ulid TaleId { get; private set; } = default!;

        public Tale Tale { get; private set; } = default!;

        private Follower() { }

        private Follower(Ulid id, Ulid followerId)
             : base(id)
        {
            FollowerId = followerId;
            FollowedAt = DateTime.UtcNow;
        }
        public static Follower Create(Ulid id, Ulid followerId)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
               new("User ID", followerId),
                 new("Follower ID", id)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Follower(id, followerId);
        }
    }
}
