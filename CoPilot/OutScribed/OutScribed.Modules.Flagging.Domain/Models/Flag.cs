using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Flagging.Domain.Models
{

    public class Flag : AggregateRoot
    {

        public DateTime FlaggedAt { get; private set; }

        public ContentType Content { get; private set; }

        public FlagType Type { get; private set; }

        public Guid FlaggerId { get; private set; }

        private Flag() { }

        private Flag(Guid contentId, ContentType content, Guid flaggerId, FlagType type)
             : base(contentId)
        {
            FlaggerId = flaggerId;
            Type = type;
            Content = content;
            FlaggedAt = DateTime.UtcNow;
        }

        public static Flag Create(Guid contentId, ContentType content, Guid flaggerId, 
            FlagType type)
        {

            var invalidFields = Validator.GetInvalidFields(
           [
                    new("Content ID", contentId),
                   new("Content Type", content),
                    new("Flagger ID", flaggerId),
                  new("Flag Reason", type),
             ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Flag(contentId, content, flaggerId, type);
        }
    }
}
