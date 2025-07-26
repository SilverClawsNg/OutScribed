using OutScribed.SharedKernel.Abstract;
using System.Security.Cryptography;

namespace OutScribed.Modules.Onboarding.Domain.Models
{

    public class OneTimeToken : ValueObject
    {
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }


        private OneTimeToken(string token, DateTime expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }

        public static OneTimeToken Generate()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(6))[..8];
            return new OneTimeToken(token, DateTime.UtcNow.AddMinutes(10));
        }

        public bool IsExpired => DateTime.UtcNow > ExpiresAt;

        public bool IsValid(string token) => Token == token && !IsExpired;


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return ExpiresAt;
        }
    }

}
