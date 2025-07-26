using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Rating.Domain.Models
{
    public class Rate : AggregateRoot
    {

        public ContentType Content { get; private set; }

        public DateTime RatedAt { get; private set; }

        public RatingType Type { get; private set; }

        public Guid RaterId { get; private set; }

        private Rate() { }

        private Rate(Guid contentId, ContentType content, Guid raterId, RatingType type)
             : base(contentId)
        {
            RaterId = raterId;
            Type = type;
            Content = content;
            RatedAt = DateTime.UtcNow;
        }

        public static Rate Create(Guid contentId, ContentType content, Guid raterId, RatingType type)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
                  new("Rater ID", raterId),
                  new("Rate Type", type),
                    new("Content ID", contentId),
                   new("Content Type", content)
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Rate(contentId, content, raterId, type);
        }
    }
}
