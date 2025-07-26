using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class LinkedTale : Entity
    {
        public Ulid TaleId { get; private set; }

        public Ulid WatchlistId { get; private set; }

        public DateTime LinkedAt { get; private set; }

        public Watchlist Watchlist { get; private set; } = default!;

        private LinkedTale() { }

        private LinkedTale(Ulid taleId)
        {
            TaleId = taleId;
            LinkedAt = DateTime.UtcNow;
        }

        public static LinkedTale Create(Ulid taleId)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
                new("Tale ID", taleId)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new LinkedTale(taleId);
        }
    }
}
