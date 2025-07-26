using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Discovery.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Discovery
{
    public class DiscoveryDbContext(DbContextOptions<DiscoveryDbContext> options)
      : DbContext(options)
    {

        public DbSet<LinkedTale> LinkedTales { get; set; }

        public DbSet<Watchlist> Watchlists { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Flag> Flags { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("discovery"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new LinkedTaleConfiguration());

            modelBuilder.ApplyConfiguration(new WatchlistConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new FlagConfiguration());

            modelBuilder.ApplyConfiguration(new FollowerConfiguration());

            modelBuilder.ApplyConfiguration(new RatingConfiguration());
        }
    }

}
