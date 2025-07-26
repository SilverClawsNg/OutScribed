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

namespace OutScribed.Modules.Onboarding.Application.Features.Commands.ResendToken
{
    public class ResendTokenService(IWriteRepository<TempUser> repository, ITempUserRepository tempUserRepository,
        IEventPublisher publisher, IJailService jail, IEmailEnqueuer email)
    {

        private readonly IWriteRepository<TempUser> _repository = repository;
        private readonly ITempUserRepository _tempUserRepository = tempUserRepository;
        private readonly IEventPublisher _publisher = publisher;
        private readonly IJailService _jail = jail;
        private readonly IEmailEnqueuer _email = email;

        public async Task ExecuteAsync(ResendTokenRequest request, HttpContext httpContext)
        {

            //Gets Ip address
            string? ipAddress = IpAddressHelper.GetClientIpAddressRobust(httpContext);

            //Checks if Ip address is null
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new GenericDomainException("Required IP address was not found.");

            //// Check if Ip Address is currently jailed
            //if (await _jail.IsCurrentlyJailedAsync(ipAddress))
            //    throw new IpAddressSuspendedException(ipAddress);

            //Checks if any entry exists with same Ip address is last 10 minutes
            // --- MODULE-SPECIFIC IP-BASED RATE LIMITING (Distinct Emails from IP) ---
            const int MAX_DISTINCT_EMAILS_PER_IP = 3;
            TimeSpan checkWindow = TimeSpan.FromMinutes(10);

            // Use the new read repository method for this specialized query
            var distinctEmailsCount = await _tempUserRepository.CountDistinctEmailsForIpInTimeWindowAsync(ipAddress, checkWindow);

            if (distinctEmailsCount >= MAX_DISTINCT_EMAILS_PER_IP)
            {
                // This IP has already tried to request tokens for too many distinct emails.
                // This is a sign of enumeration or abuse within this module's context.

                await _publisher.IpAddressViolated_Jail(ipAddress, request.EmailAddress!, JailReason.TooManyTokenRequests);

                throw new TooManyDistinctEmailsFromIpException(
                    ipAddress,
                    MAX_DISTINCT_EMAILS_PER_IP,
                    checkWindow,
                    request.EmailAddress
                );
            }

            //Create Specification
            var existingSpec = new TempUserByIdSpecification(request.Id!.Value);
            //Evaluate Specification
            var tempUser = await _repository.FirstOrDefaultAsync(existingSpec) 
                ?? throw new DoesNotExistException(request.Id!.Value, "TempUser"); // Check for existence

            // Critical: Check if the TempUser is currently locked out before processing
            if (tempUser.IsCurrentlyLockedOut)
            {
                throw new CurrentlyLockedOutException(
                    tempUser.EmailAddress,
                    tempUser.TimeUntilLockoutEnds,
                    tempUser.Id
                );
            }

            // Delegate to the TempUser aggregate to handle the resend logic (no email update here)
            DomainResults result = tempUser.HandleResendRequest(request.EmailAddress); // No email update on this path

            await _repository.SaveAsync(); // Save changes here before potential exception

            switch (result)
            {
                case DomainResults.TooSoon:
                    throw new ResendTooSoonException(
                        tempUser.EmailAddress,
                        tempUser.TimeUntilLockoutEnds,
                        tempUser.Id
                    );
                case DomainResults.TooManyResends:
                    throw new TooManyResendsException(
                        tempUser.EmailAddress,
                        tempUser.TimeUntilLockoutEnds,
                        tempUser.Id
                    );
                case DomainResults.Success:
                    break;
                case DomainResults.CurrentlyLockedOut:
                    throw new CurrentlyLockedOutException(
                        tempUser.EmailAddress,
                        tempUser.TimeUntilLockoutEnds,
                        tempUser.Id
                    );
                default:
                    throw new InvalidOperationException("An unexpected error occurred during token resend.");
            }

            //broke from switch i.e. success
            //schedule mail
            _email.EnqueueTempUserResendTokenEmail(tempUser.EmailAddress, tempUser.Token.Token);

        }

    }
}
