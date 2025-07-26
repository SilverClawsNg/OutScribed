using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class Source : ValueObject
    {

        public string Text { get; private set; } = string.Empty;

        public string Url { get; private set; } = string.Empty;

        private Source() { }

        private Source(string url, string text)
        {
            Url = url;
            Text = text;
        }

        public static Source Create(string text, string url)
        {

            // Perform validation checks
          
            var invalidFields = Validator.GetInvalidFields(
             [
                  new("Source Text", text, maxLength: 28),
                  new("Source URL", url, maxLength: 255),
             ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Source(text, url);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;

            yield return Url;

        }
    }
  
}