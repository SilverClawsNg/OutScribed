using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class Notification : Entity
    {
        public Ulid AccountId { get; set; }

        public Account Account { get; private set; } = default!;

        public DateTime HappenedAt { get; set; }

        public string Text { get; set; } = string.Empty;

        public NotificationType Type { get; set; }

        public ContentType Content { get; set; }

        public bool HasRead { get; set; }

        private Notification() { }

        private Notification(string text, NotificationType type, ContentType content)
        {
            HappenedAt = DateTime.UtcNow;
            Text = text;
            HasRead = false;
            Type = type;
            Content = content;
        }

        public static Notification Create(string text, NotificationType type,
           ContentType content)
        {

            var invalidFields = Validator.GetInvalidFields(
            [
                  new("Notification Text", text, maxLength: 255),
                  new("Notification Type", type),
                  new("Content Type", content)
            ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Notification(text, type, content);
        }
    }
}
