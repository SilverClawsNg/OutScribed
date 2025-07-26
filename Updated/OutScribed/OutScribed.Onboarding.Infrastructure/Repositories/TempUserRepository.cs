using OutScribed.Onboarding.Domain.Interfaces;
using OutScribed.Onboarding.Domain.Entities;
using OutScribed.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OutScribed.Onboarding.Infrastructure.Repositories
{
    public class TempUserRepository : ITempUserRepository
    {
        private readonly OutScribedDbContext _context;

        public TempUserRepository(OutScribedDbContext context)
        {
            _context = context;
        }

        public async Task<TempUser?> GetByEmailAsync(string email)
        {
            return await _context.TempUsers
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TempUser user)
        {
            await _context.TempUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TempUser user)
        {
            _context.TempUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountRequestsFromIpAsync(string ipAddress, TimeSpan within)
        {
            var cutoff = DateTime.UtcNow.Subtract(within);
            return await _context.TempUsers
                .Where(u => u.IpAddress == ipAddress && u.RequestedAt >= cutoff)
                .CountAsync();
        }

        public async Task<int> CountRequestsFromUserAsync(string email, TimeSpan within)
        {
            var cutoff = DateTime.UtcNow.Subtract(within);
            return await _context.TempUsers
                .Where(u => u.Email == email && u.RequestedAt >= cutoff)
                .CountAsync();
        }
    }
}