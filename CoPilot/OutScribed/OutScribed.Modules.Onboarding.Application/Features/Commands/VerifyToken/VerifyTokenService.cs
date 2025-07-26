using Microsoft.AspNetCore.Http;
using OutScribed.Modules.Jail.Application.Interfaces;
using OutScribed.Modules.Onboarding.Application.Repository;
using OutScribed.Modules.Onboarding.Domain.Enums;
using OutScribed.Modules.Onboarding.Domain.Exceptions;
using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.Modules.Onboarding.Domain.Specifications;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Exceptions;
using OutScribed.SharedKernel.Interfaces;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Onboarding.Application.Features.Commands.VerifyToken
{
    public class VerifyTokenService(IWriteRepository<TempUser> repository, ITempUserRepository tempUserRepository,
        IEventPublisher publisher, IJailService jail, IEmailEnqueuer email)
    {

        private readonly IWriteRepository<TempUser> _repository = repository;
        private readonly ITempUserRepository _tempUserRepository = tempUserRepository;
        private readonly IEventPublisher _publisher = publisher;
        private readonly IJailService _jail = jail;
        private readonly IEmailEnqueuer _email = email;

        public async Task ExecuteAsync(VerifyTokenRequest request, HttpContext httpContext)
        {

            //Gets Ip address
            string? ipAddress = IpAddressHelper.GetClientIpAddressRobust(httpContext);

            //Checks if Ip address is null
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new GenericDomainException("Required IP address was not found.");

            //// Check if Ip Address is currently jailed
            //if (await _jail.IsCurrentlyJailedAsync(ipAddress))
            //    throw new IpAddressSuspendedException(ipAddress);

            //Create Specification
            var existingSpec = new TempUserByIdSpecification(request.Id!.Value);
            //Evaluate Specification
            var tempUser = await _repository.FirstOrDefaultAsync(existingSpec) 
                ?? throw new DoesNotExistException(request.Id!.Value, "TempUser"); // Check for existence

            // Check if the account is already verified
            if (tempUser.IsVerified)
                throw new AlreadyVerifiedException(request.Id!.Value, request.EmailAddress);

            // Critical: Check if the TempUser is currently locked out before processing
            if (tempUser.IsCurrentlyLockedOut)
            {
                throw new CurrentlyLockedOutException(
                    tempUser.EmailAddress,
                    tempUser.TimeUntilLockoutEnds,
                    tempUser.Id
                );
            }

            // Delegate to the TempUser aggregate to handle the verification logic
            DomainResults result = tempUser.HandleVerificationAttempt(request.Token);

            await _repository.SaveAsync(); // *** SAVE CHANGES HERE BEFORE POTENTIAL EXCEPTION ***

            // Now, evaluate the result and throw exceptions if necessary
            switch (result)
            {
                case DomainResults.TooManyVerificationAttempts:
                    throw new TooManyVerificationAttemptsException(
                          tempUser.EmailAddress,
                          tempUser.TimeUntilLockoutEnds,
                          tempUser.Id
                      );
                case DomainResults.InvalidTokenOrExpired:
                    // This is a common failure, you might want a specific error message for UX
                    throw new InvalidOperationException("Invalid or expired verification token.");
                case DomainResults.Success:
                    // All good, continue
                    break;
                case DomainResults.CurrentlyLockedOut: // Should be caught by initial check
                    throw new CurrentlyLockedOutException(
                           tempUser.EmailAddress,
                           tempUser.TimeUntilLockoutEnds,
                           tempUser.Id
                       );
                default:
                    throw new InvalidOperationException("An unexpected error occurred during token verification.");
            }

        }

    }
}
