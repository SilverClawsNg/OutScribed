using OutScribed.Modules.Jail.Application.Interfaces;
using OutScribed.Modules.Jail.Domain.Models;
using OutScribed.Modules.Jail.Domain.Specifications;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Jail.Application.Services
{
    public class JailService(IWriteRepository<JailedIpAddress> jailedIpRepository) 
        : IJailService
    {
        private readonly IWriteRepository<JailedIpAddress> _repository = jailedIpRepository;

        public async Task ProcessViolationAsync(string ipAddress, string emailAddress, JailReason reason)
        {
            // First, find if this IP is already jailed
            var spec = new JailedIpAddressByIpSpecification(ipAddress);

            var jailedIp = await _repository.FirstOrDefaultAsync(spec);

            if (jailedIp == null)
            {
                //First time jail for this IP: Create a temporary jail
                var initialJailDuration = TimeSpan.FromMinutes(1); // Start with 1 minute
                var newJail = JailedIpAddress.CreateTemporaryJail(IdGenerator.Generate(), ipAddress, reason, initialJailDuration);
                await _repository.AddAsync(newJail);
            }
            else
            {
                jailedIp.CheckAndExpire(); // Ensure status is up-to-date

                if (jailedIp.CurrentStatus == JailStatus.ActivePermanent)
                {
                    return; // Already permanently banned, no further action for this specific violation.
                }

                if (jailedIp.TemporaryJailCount < 3) // Example: 1st, 2nd, 3rd strikes
                {
                    // Determine new duration or permanent ban based on existing count
                    TimeSpan nextDuration = TimeSpan.FromMinutes(jailedIp.TemporaryJailCount * 5);
                    jailedIp.ExtendTemporaryJail(reason, nextDuration);

                }
                else // Example: After 3 temporary jails, consider permanent
                {
                    // This is your permanent ban criteria
                    jailedIp.PermanentlyBan(reason);
                }

                await _repository.UpdateAsync(jailedIp);
            }

            await _repository.SaveAsync();
        }

        public async Task<bool> IsCurrentlyJailedAsync(string ipAddress)
        {
            var spec = new JailedIpAddressByIpSpecification(ipAddress);

            var jailedIp = await _repository.FirstOrDefaultAsync(spec);

            if (jailedIp == null)
            {
                return false;
            }

            // Always check/expire before determining status
            jailedIp.CheckAndExpire();
            await _repository.SaveAsync(); // Save the status change if expired

            return jailedIp.IsCurrentlyJailed();
        }
    }
}
