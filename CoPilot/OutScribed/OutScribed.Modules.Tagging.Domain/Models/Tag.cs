using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Tagging.Domain.Models
{

    public class Tag : AggregateRoot
    {

        public DateTime CreatedAt { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Slug { get; private set; } = string.Empty;

        private Tag() { }

        private Tag(Ulid id, string name, string slug)
            : base(id)
        {
            Name = name;
            Slug = slug;
            CreatedAt = DateTime.UtcNow;
        }

        public static Tag Create(Ulid id, string name)
        {

            var invalidFields = Validator.GetInvalidFields(
                [
                   new("Tag ID", id),
                   new("Name", name, maxLength: 30)
                ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Tag(id, TagSlugHelper.Clean(name), TagSlugHelper.Slugify(name));
        }
    }
   
}
