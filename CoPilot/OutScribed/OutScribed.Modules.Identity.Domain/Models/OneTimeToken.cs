using OutScribed.SharedKernel.Abstract;
using System.Security.Cryptography;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class OneTimeToken : ValueObject
    {
        public string Token { get; init; } = string.Empty;

        public DateTime ExpiresAt { get; init; }

        public bool DoNotResendOtp { get; init; }

        private OneTimeToken() { }

        private OneTimeToken(string token)
        {
            Token = token;
            ExpiresAt = DateTime.UtcNow.AddMinutes(10);
        }

        public static OneTimeToken Generate()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(6))[..8];
            return new OneTimeToken(token);
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
