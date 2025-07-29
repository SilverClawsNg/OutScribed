using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class RefreshToken : ValueObject
    {
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        private RefreshToken() { }

        private RefreshToken(string token)
        {
            Token = token;
            ExpiresAt = DateTime.UtcNow.AddMinutes(720);
        }

        public static RefreshToken Create(string token)
        {

            var invalidFields = Validator.GetInvalidFields(
              [
                  new("Refresh Token", token)
                ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new RefreshToken(token);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return ExpiresAt;
        }
    }
}
