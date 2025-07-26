using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class Follower : Entity
    {
    
        public DateTime FollowedAt { get; private set; }

        public Ulid FollowerId { get; private set; }


        public Ulid WatchlistId { get; private set; } = default!;

        public Watchlist Watchlist { get; private set; } = default!;

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
