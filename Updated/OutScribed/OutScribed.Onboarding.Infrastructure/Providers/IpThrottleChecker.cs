using OutScribed.Onboarding.Domain.Interfaces;

namespace OutScribed.Onboarding.Infrastructure.Providers
{
    public class IpThrottleChecker : IIpThrottleChecker
    {
        private readonly ITempUserRepository _repository;

        public IpThrottleChecker(ITempUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsBlockedAsync(string ipAddress, string email)
        {
            var ipCount = await _repository.CountRequestsFromIpAsync(ipAddress, TimeSpan.FromMinutes(15));
            var userCount = await _repository.CountRequestsFromUserAsync(email, TimeSpan.FromHours(1));

            return ipCount > 5 || userCount > 3;
        }
    }
}
