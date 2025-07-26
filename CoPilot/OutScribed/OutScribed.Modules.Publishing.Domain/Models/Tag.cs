using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Publishing.Domain.Models
{
    public class Tag : Entity
    {

        public DateTime TaggedAt { get; private set; }

        public Ulid TagId { get; private set; }


        public Ulid TaleId { get; private set; } = default!;

        public Tale Tale { get; private set; } = default!;


        private Tag() { }

        private Tag(Ulid id, Ulid tagId)
            : base(id)
        {
            TaggedAt = DateTime.UtcNow;
            TagId = tagId;
        }

        public static Tag Create(Ulid id, Ulid tagId)
        {

            var invalidFields = Validator.GetInvalidFields(
            [
                    new("Tale Tag ID", id),
                    new("Tag ID", tagId)
             ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Tag(id, tagId);
        }
    }
}
