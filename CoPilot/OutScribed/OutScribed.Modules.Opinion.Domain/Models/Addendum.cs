using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Analysis.Domain.Models
{
    public class Addendum : Entity
    {
        public Ulid InsightId { get; private set; }
        public Insight Insight { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public string Text { get; private set; } = string.Empty;

        private Addendum() { }

        private Addendum(string text)
        {
            CreatedAt = DateTime.UtcNow;
            Text = text;
        }

        public static Addendum Create(string text)
        {

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Addendum Text", text, minLength: 30, maxLength: 4096),
               ]
            );
           
            if ((invalidFields.Count) != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Addendum(HtmlContentProcessor.Clean(text));

        }

    }
}
