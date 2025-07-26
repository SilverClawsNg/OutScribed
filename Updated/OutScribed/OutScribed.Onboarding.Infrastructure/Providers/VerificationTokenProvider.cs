using OutScribed.Onboarding.Domain.Interfaces;

namespace OutScribed.Onboarding.Infrastructure.Providers
{
    public class VerificationTokenProvider : ITokenProvider
    {
        public string GenerateToken()
        {
            var bytes = new byte[16];
            Random.Shared.NextBytes(bytes);
            return Convert.ToHexString(bytes); // Produces a 32-char hex token
        }
    }
}