using OutScribed.Modules.Jail.Application.Interfaces;
using OutScribed.Modules.Jail.Domain.Models;
using OutScribed.Modules.Jail.Domain.Specifications;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Utilities;
using System.Net;

namespace OutScribed.Modules.Jail.Application.Services
{
    public class JailService(IWriteRepository<IpAddress> jailedIpRepository) 
        : IJailService
    {
        private readonly IWriteRepository<IpAddress> _repository = jailedIpRepository;

        public async Task ProcessViolationAsync(string ipAddress, string emailAddress, JailReason reason)
        {
            // First, find if this IP is already jailed
            var spec = new IpAddressByValueSpec(ipAddress);
            var jailedIp = await _repository.FirstOrDefaultAsync(spec);

            if (jailedIp == null)
            {
                //First time jail for this IP: Create a temporary jail
                jailedIp = IpAddress.Create(IdGenerator.Generate(), ipAddress, reason, DateTime.UtcNow);
                await _repository.AddAsync(jailedIp);
            }
            else
            {
                // Existing jailed IP: Handle repeated violation
                //If it fails, no action is taken
                jailedIp.HandleRepeatedViolation(reason, DateTime.UtcNow);

                await _repository.UpdateAsync(jailedIp);
            }

            await _repository.SaveAsync();
        }

        public async Task<bool> IsCurrentlyJailedAsync(string ipAddress)
        {
            var spec = new IpAddressByValueSpec(ipAddress);
            var jailedIp = await _repository.FirstOrDefaultAsync(spec);

            if (jailedIp == null)
            {
                return false;
            }

            return jailedIp.IsCurrentlyJailed(DateTime.UtcNow);
        }

        public async Task ReleaseIpAddress(string ipAddress)
        {
            var spec = new IpAddressByValueSpec(ipAddress);
            var jailedIp = await _repository.FirstOrDefaultAsync(spec);

            if (jailedIp == null)
            {
                // Still important to handle non-existent case, though no log/event as per request.
                return;
            }

            var result = jailedIp.UnjailManually(DateTime.UtcNow);

            // Only save if a state change actually occurred
            if (result)
            {
                await _repository.UpdateAsync(jailedIp);

                await _repository.SaveAsync();
            }
            // No logging or event publishing here either, for consistency.
        }
    }
}
