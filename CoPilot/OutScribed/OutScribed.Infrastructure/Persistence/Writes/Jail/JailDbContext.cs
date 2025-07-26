using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Jail.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Jail
{
    public class JailDbContext(DbContextOptions<JailDbContext> options)
     : DbContext(options)
    {
        public DbSet<JailedIpAddress> JailedIpAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("jail"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new JailedIpAddressConfiguration());

        }

    }
}
