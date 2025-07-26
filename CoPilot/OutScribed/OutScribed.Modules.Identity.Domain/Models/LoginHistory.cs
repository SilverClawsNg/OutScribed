using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class LoginHistory : Entity
    {
        public Ulid AccountId { get; private set; }

        public Account Account { get; private set; } = default!;

        public DateTime LoggedAt { get; private set; }

        public string IpAddress { get; private set; } = string.Empty;

        private LoginHistory() { }

        private LoginHistory(string ipAddress) 
        {
            LoggedAt = DateTime.UtcNow;
            IpAddress = ipAddress;
        }

        public static LoginHistory Create(string IpAddress)
        {

            var invalidFields = Validator.GetInvalidFields(
               [
                     new("IP Address", IpAddress, maxLength: 32)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new LoginHistory(IpAddress);
        }
    }
}
