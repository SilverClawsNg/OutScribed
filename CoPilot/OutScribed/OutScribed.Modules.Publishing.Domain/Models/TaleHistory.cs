using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Domain.Models
{
    public class TaleHistory : Entity
    {
        public Ulid TaleId { get; private set; }
        public Tale Tale { get; private set; } = default!;
        public DateTime HappendedAt { get; private set; }
        public Ulid AdminId { get; private set; }
        public TaleStatus Status { get; private set; }
        public string? Notes { get; private set; }

        private TaleHistory() { }

        private TaleHistory(TaleStatus status, Ulid creatorId, string? notes)
        {
            Status = status;
            AdminId = creatorId;
            HappendedAt = DateTime.UtcNow;
            Notes = notes;
        }

        public static TaleHistory Create(TaleStatus status, Ulid creatorId, string? notes)
        {
            var invalidFields = Validator.GetInvalidFields(
              [
                  new("Tale Status", status),
                  new("Creator ID", creatorId),
                  new("History Notes", notes, minLength: 3, maxLength: 2048, isOptional:true)
              ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new TaleHistory(status, creatorId, notes);
        }
    }
}
