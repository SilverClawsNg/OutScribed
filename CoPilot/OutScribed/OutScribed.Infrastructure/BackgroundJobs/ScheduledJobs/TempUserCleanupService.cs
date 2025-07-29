using Microsoft.Extensions.Logging;
using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.Modules.Onboarding.Domain.Specifications;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;

namespace OutScribed.Infrastructure.BackgroundJobs.ScheduledJobs
{
    public class TempUserCleanupService
    {
        private readonly IWriteRepository<TempUser> _tempUserRepository;
        private readonly ILogger<TempUserCleanupService> _logger;

        public TempUserCleanupService(IWriteRepository<TempUser> tempUserRepository, ILogger<TempUserCleanupService> logger)
        {
            _tempUserRepository = tempUserRepository;
            _logger = logger;
        }

        public async Task CleanExpiredTempUsersAsync()
        {
            _logger.LogInformation("Starting TempUser cleanup job.");

            // Define the cleanup criteria: LastUpdated older than 24 hours
            var cutoffTime = DateTime.UtcNow.AddHours(-24);
            // Use the updated specification
            var cleanupSpec = new TempUserExpiredSpecification(cutoffTime);

            var usersToDelete = await _tempUserRepository.ListAsync(cleanupSpec);
            int deletedCount = 0;

            if (usersToDelete.Count != 0) // Only proceed if there are users to delete
            {
                // Consider adding a bulk delete method to your IRepository for efficiency
                // e.g., await _tempUserRepository.DeleteRangeAsync(usersToDelete.Select(u => u.Id));
                foreach (var user in usersToDelete)
                {
                    try
                    {
                        await _tempUserRepository.DeleteAsync(user);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to delete TempUser {TempUserId} during cleanup.", user.Id);
                        // Continue to the next user, don't let one failure stop the job
                    }
                }
                await _tempUserRepository.SaveAsync(); // Save changes after the batch/all deletions
            }

            _logger.LogInformation("Finished TempUser cleanup job. Deleted {Count} expired TempUsers.", deletedCount);
        }
    }
}