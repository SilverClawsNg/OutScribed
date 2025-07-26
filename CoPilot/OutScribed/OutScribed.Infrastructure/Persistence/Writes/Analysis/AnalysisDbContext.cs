using Microsoft.EntityFrameworkCore;
using OutScribed.Modules.Analysis.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{

    public class AnalysisDbContext(DbContextOptions<AnalysisDbContext> options) 
        : DbContext(options)
    {

        public DbSet<Addendum> Addendums { get; set; }

        public DbSet<Insight> Insights { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Flag> Flags { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Share> Shares { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("analysis"); // Default schema for this context

            // Configurations 
            modelBuilder.ApplyConfiguration(new AddendumConfiguration());

            modelBuilder.ApplyConfiguration(new InsightConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new FlagConfiguration());

            modelBuilder.ApplyConfiguration(new FollowerConfiguration());

            modelBuilder.ApplyConfiguration(new RatingConfiguration());

            modelBuilder.ApplyConfiguration(new ShareConfiguration());

        }
    }
}
