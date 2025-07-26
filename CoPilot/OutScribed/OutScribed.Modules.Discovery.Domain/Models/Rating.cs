using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class Rating : Entity
    {

        public DateTime RatedAt { get; private set; }

        public RatingType Type { get; private set; }

        public Ulid RaterId { get; private set; }


        public Ulid WatchlistId { get; private set; } = default!;

        public Watchlist Watchlist { get; private set; } = default!;


        private Rating() { }

        private Rating(Ulid id, Ulid raterId, RatingType type)
             : base(id)
        {
            RaterId = raterId;
            Type = type;
            RatedAt = DateTime.UtcNow;
        }

        public static Rating Create(Ulid id, Ulid raterId, RatingType type)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
                  new("Ratingr ID", raterId),
                  new("Rating Type", type),
                    new("Rating ID", id)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Rating(id, raterId, type);
        }
    }
}
