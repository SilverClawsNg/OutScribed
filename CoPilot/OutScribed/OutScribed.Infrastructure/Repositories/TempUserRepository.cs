using Microsoft.EntityFrameworkCore;
using OutScribed.Infrastructure.Persistence.Writes.Onboarding;
using OutScribed.Modules.Onboarding.Application.Repository;

namespace OutScribed.Infrastructure.Repositories
{
    public class TempUserRepository(OnboardingDbContext dbContext) : ITempUserRepository
    {
        private readonly OnboardingDbContext _dbContext = dbContext;

        public async Task<int> CountDistinctEmailsForIpInTimeWindowAsync(string ipAddress, TimeSpan timeWindow)
        {
            var cutoffTime = DateTime.UtcNow.Subtract(timeWindow);

            return await _dbContext.TempUsers
                                   .AsNoTracking() // Important for reads: no change tracking needed
                                   .Where(tu => tu.IpAddress == ipAddress && tu.LastUpdated >= cutoffTime)
                                   .Select(tu => tu.EmailAddress) // Project to email addresses
                                   .Distinct() // Get only distinct ones
                                   .CountAsync(); // Count them
        }
    }
}
