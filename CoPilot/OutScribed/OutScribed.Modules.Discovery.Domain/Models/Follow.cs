using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class Follow : Entity
    {
        public DateTime FollowedAt { get; private set; }
        public Ulid FollowId { get; private set; }
        public Ulid WatchlistId { get; private set; } = default!;
        public Watchlist Watchlist { get; private set; } = default!;

        private Follow() { }

        private Follow(Ulid id, Ulid followId)
             : base(id)
        {
            FollowId = followId;
            FollowedAt = DateTime.UtcNow;
        }
        public static Follow Create(Ulid id, Ulid followId)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
               new("User ID", followId),
                 new("Follow ID", id)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Follow(id, followId);
        }
    }
}
