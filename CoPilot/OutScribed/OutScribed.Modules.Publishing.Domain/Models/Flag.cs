using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Domain.Models
{

    public class Flag : Entity
    {
        public DateTime FlaggedAt { get; private set; }
        public FlagType Type { get; private set; }
        public Ulid FlaggerId { get; private set; }
        public Ulid TaleId { get; private set; } = default!;
        public Tale Tale { get; private set; } = default!;

        private Flag() { }

        private Flag(Ulid id, Ulid flaggerId, FlagType type)
             : base(id)
        {
            FlaggerId = flaggerId;
            Type = type;
            FlaggedAt = DateTime.UtcNow;
        }

        public static Flag Create(Ulid id, Ulid flaggerId, FlagType type)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
                    new("Flag ID", id),
                    new("Flagger ID", flaggerId),
                  new("Flag Reason", type),
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Flag(id, flaggerId, type);
        }
    }
}
