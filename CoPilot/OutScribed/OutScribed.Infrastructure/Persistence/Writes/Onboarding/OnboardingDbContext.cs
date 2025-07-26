using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Onboarding.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Onboarding
{
    public class OnboardingDbContext(DbContextOptions<OnboardingDbContext> options)
     : DbContext(options)
    {

        public DbSet<TempUser> TempUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("onboarding"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new TempUserConfiguration());

        }
    }

}
