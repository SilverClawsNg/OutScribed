using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Tagging.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Tagging
{
    public class TaggingDbContext(DbContextOptions<TaggingDbContext> options)
     : DbContext(options)
    {

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("tagging"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new TagConfiguration());

        }
    }

}
