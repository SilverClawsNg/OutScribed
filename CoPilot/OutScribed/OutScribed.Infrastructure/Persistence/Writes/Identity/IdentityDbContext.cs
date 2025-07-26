using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Identity.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Identity
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
     : DbContext(options)
    {

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<LoginHistory> LoginHistory { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Profile> Profile { get; set; }

        public DbSet<WriterInfo> WriterInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("analysis"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new AccountConfiguration());

            modelBuilder.ApplyConfiguration(new ContactConfiguration());

            modelBuilder.ApplyConfiguration(new LoginHistoryConfiguration());

            modelBuilder.ApplyConfiguration(new NotificationConfiguration());

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());

            modelBuilder.ApplyConfiguration(new WriterInfoConfiguration());

        }
    }

}
