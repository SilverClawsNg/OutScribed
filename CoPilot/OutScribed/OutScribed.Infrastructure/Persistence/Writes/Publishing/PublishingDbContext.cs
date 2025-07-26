using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Publishing
{
    public class PublishingDbContext(DbContextOptions<PublishingDbContext> options)
     : DbContext(options)
    {

        public DbSet<Tale> Tales { get; set; }

        public DbSet<TaleHistory> TaleHistories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Flag> Flags { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Share> Shares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("publishing"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new TaleConfiguration());

            modelBuilder.ApplyConfiguration(new TaleHistoryConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new FlagConfiguration());

            modelBuilder.ApplyConfiguration(new FollowerConfiguration());

            modelBuilder.ApplyConfiguration(new RatingConfiguration());

            modelBuilder.ApplyConfiguration(new ShareConfiguration());

        }
    }

}
