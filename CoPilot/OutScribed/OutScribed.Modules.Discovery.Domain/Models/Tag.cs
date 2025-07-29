using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class Tag : Entity
    {
        public DateTime TaggedAt { get; private set; }
        public Ulid WatchlistId { get; private set; } = default!;
        public Watchlist Watchlist { get; private set; } = default!;

        private Tag() { }

        private Tag(Ulid tagId)
            : base(tagId)
        {
            TaggedAt = DateTime.UtcNow;
        }

        public static Tag Create(Ulid tagId)
        {

            var invalidFields = Validator.GetInvalidFields(
            [
                    new("Tag ID", tagId)
             ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Tag(tagId);
        }
    }
}
