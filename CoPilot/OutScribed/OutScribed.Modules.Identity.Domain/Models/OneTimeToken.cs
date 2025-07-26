using OutScribed.SharedKernel.Abstract;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class OneTimeToken : ValueObject
    {
        public int Token { get; init; }

        public DateTime ExpiresAt { get; init; }

        public bool DoNotResendOtp { get; init; }

        private OneTimeToken() { }

        private OneTimeToken(int token)
        {
            Token = token;
            ExpiresAt = DateTime.UtcNow.AddMinutes(10);
        }

        public static OneTimeToken Create()
        {
            return new OneTimeToken(new Random().Next(123456, 987654));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return ExpiresAt;
        }
    }
}
