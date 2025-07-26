using Microsoft.EntityFrameworkCore;
using OutScribed.Onboarding.Domain.Entities;
// + other modules’ entity imports

namespace OutScribed.Infrastructure.Persistence
{
    public class OutScribedDbContext : DbContext
    {
        public DbSet<TempUser> TempUsers => Set<TempUser>();
        //public DbSet<Account> Accounts => Set<Account>();
        // Other aggregates...

        public OutScribedDbContext(DbContextOptions<OutScribedDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply model configurations from each module
            builder.ApplyConfigurationsFromAssembly(typeof(OutScribedDbContext).Assembly);
        }
    }
}