using MediatR;
using OutScribed.Onboarding.Domain.Entities;
using OutScribed.Onboarding.Domain.Interfaces;
using OutScribed.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Onboarding.Application.Features.CreateTempUser
{
    public class CreateTempUserHandler : IRequestHandler<CreateTempUserCommand>
    {
        private readonly ITempUserRepository _repo;
        private readonly IEmailSender _email;
        private readonly IIpThrottleChecker _throttle;

        public CreateTempUserHandler(ITempUserRepository repo, IEmailSender email, IIpThrottleChecker throttle)
        {
            _repo = repo;
            _email = email;
            _throttle = throttle;
        }


        public async Task<Unit> Handle(CreateTempUserCommand request, CancellationToken ct)
        {
            if (await _throttle.IsBlockedAsync(request.IpAddress, request.Email))
                throw new InvalidOperationException("Rate limit exceeded.");

            var user = await _repo.GetByEmailAsync(request.Email);
            if (user != null)
            {
                user.ResendOtp();
                await _repo.UpdateAsync(user);
            }
            else
            {
                user = new TempUser(request.Email, request.IpAddress);
                await _repo.CreateAsync(user);
            }

            await _email.SendOtpAsync(user.Email, user.Otp.Code);
            return Unit.Value;
        }
    }
}
