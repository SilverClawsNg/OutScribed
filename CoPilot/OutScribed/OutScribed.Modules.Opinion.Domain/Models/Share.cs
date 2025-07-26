using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Domain.Models
{
    public class Share : Entity
    {

        public DateTime SharedAt { get; private set; }

        public Ulid? SharerId { get; private set; }

        public ContactType Type { get; private set; }

        public string? Handle { get; private set; } = default!;


        public Ulid InsightId { get; private set; } = default!;

        public Insight Insight { get; private set; } = default!;


        private Share() { }

        private Share(Ulid id, Ulid sharerId, ContactType contact)
            : base(id)
        {
            SharerId = sharerId;
            Type = contact;
            SharedAt = DateTime.UtcNow;
        }

        public static Share Create(Ulid id, Ulid sharerId, ContactType contact, string handle)
        {

            var invalidFields = Validator.GetInvalidFields(
            [
                 new("Sharer ID", sharerId),
                  new("Type Type", contact),
                    new("Share ID", id),
                    new("Handle", handle, maxLength: 64, isOptional: true)
             ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Share(id, sharerId, contact);
        }
    }
}
