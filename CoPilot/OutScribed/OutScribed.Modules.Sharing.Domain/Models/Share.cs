using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Sharing.Domain.Models
{
    public class Share : AggregateRoot
    {
        public ContentType Content { get; private set; }

        public DateTime SharedAt { get; private set; }

        public Guid SharerId { get; private set; }

        public ContactType Contact { get; private set; }

        private Share() { }

        private Share(Guid contentId, ContentType content, Guid sharerId, ContactType contact)
            : base(contentId)
        {
            SharerId = sharerId;
            Contact = contact;
            Content = content;
            SharedAt = DateTime.UtcNow;
        }

        public static Share Create(Guid contentId, ContentType content, Guid sharerId, ContactType contact)
        {

            var invalidFields = Validator.GetInvalidFields(
            [
                 new("Shared ID", sharerId),
                  new("Contact Type", contact),
                    new("Content ID", contentId),
                   new("Content Type", content)
             ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Share(contentId, content, sharerId, contact);
        }
    }
}
