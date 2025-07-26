using OutScribed.Onboarding.Application.Interfaces;
using OutScribed.Onboarding.Domain.Entities;
using OutScribed.Onboarding.Domain.Interfaces;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.Onboarding.Application.Services
{
    public class OnboardingService : IOnboardingService
    {
        private readonly ITempUserRepository _repository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IIpThrottleChecker _ipThrottle;

        public OnboardingService(
            ITempUserRepository repository,
            ITokenProvider tokenProvider,
            IEmailSender emailSender,
            IIpThrottleChecker ipThrottle)
        {
            _repository = repository;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _ipThrottle = ipThrottle;
        }

        public async Task RequestAccessAsync(string email, string ipAddress)
        {
            if (await _ipThrottle.IsBlockedAsync(ipAddress, email))
                throw new InvalidOperationException("Too many requests from this IP or user. Try later.");

            var existing = await _repository.GetByEmailAsync(email);

            if (existing != null)
            {
                existing.ResendOtp();
                await _repository.UpdateAsync(existing);
                await _emailSender.SendOtpAsync(email, existing.Otp.Code);
                return;
            }

            var user = new TempUser(email, ipAddress);
            await _repository.CreateAsync(user);
            await _emailSender.SendOtpAsync(email, user.Otp.Code);
        }

        public async Task<bool> VerifyAsync(string email, int code)
        {
            var user = await _repository.GetByEmailAsync(email)
                        ?? throw new KeyNotFoundException("User not found.");

            user.Verify(code);
            await _repository.UpdateAsync(user);
            return user.IsVerified;
        }
    }
}