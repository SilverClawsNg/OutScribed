using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Analysis.Domain.Models
{
    public class Tag : Entity
    {
        public DateTime TaggedAt { get; private set; }
        public Ulid InsightId { get; private set; } = default!;
        public Insight Insight { get; private set; } = default!;

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

            //raise event

            return new Tag(tagId);
        }
    }
}
