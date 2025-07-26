using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class Writer : Entity
    {
        public Ulid AccountId { get; private set; }

        public string Address { get; private set; } = string.Empty;

        public Country Country { get; private set; }

        public string Application { get; private set; } = string.Empty;

        public DateTime AppliedAt { get; private set; }

        public DateTime ApprovedAt { get; private set; }

        public bool IsActive { get; private set; }

        private Writer() { }

        private Writer(string address, Country country, string application)
        {
            Address = address;
            Country = country;
            Application = application;
            AppliedAt = DateTime.UtcNow;
        }

        public static Writer Create(string address, Country country,
          string application)
        {
            var invalidFields = Validator.GetInvalidFields(
             [
                   new("Writer Address", address, maxLength: 255),
                   new("Application URL", application, maxLength: 60),
                   new("Writer Country", country)
             ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Writer(address, country, application);

        }
    }
}
