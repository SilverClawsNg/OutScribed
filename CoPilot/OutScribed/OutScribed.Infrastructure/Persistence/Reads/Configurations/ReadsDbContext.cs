using Microsoft.EntityFrameworkCore;
using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.Application.Queries.DTOs.Discovery;
using OutScribed.Application.Queries.DTOs.Identity;
using OutScribed.Application.Queries.DTOs.Publishing;
using OutScribed.Application.Queries.DTOs.Tagging;
using OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis;
using OutScribed.Infrastructure.Persistence.Reads.Configurations.Discovery;
using OutScribed.Infrastructure.Persistence.Reads.Configurations.Identity;
using OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing;
using OutScribed.Infrastructure.Persistence.Reads.Configurations.Tagging;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations
{
    public class ReadsDbContext(DbContextOptions<ReadsDbContext> options)
        : DbContext(options)
    {

        //Tags
        public DbSet<Tag> Tags { get; set; }


        //Insights
        public DbSet<InsightAddendum> InsightAddendums { get; set; }

        public DbSet<InsightComment> InsightComments { get; set; }

        public DbSet<InsightDetail> InsightDetails { get; set; }

        public DbSet<InsightFlag> InsightFlags { get; set; }

        public DbSet<InsightFollow> InsightFollows { get; set; }

        public DbSet<InsightList> InsightLists { get; set; }

        public DbSet<InsightRating> InsightRatings { get; set; }

        public DbSet<InsightShare> InsightShares { get; set; }


        //Watchlists
        public DbSet<WatchlistDetail> WatchlistDetails { get; set; }

        public DbSet<WatchlistComment> WatchlistComments { get; set; }

        public DbSet<WatchlistFlag> WatchlistFlags { get; set; }

        public DbSet<WatchlistRating> WatchlistRatings { get; set; }

        public DbSet<WatchlistFollow> WatchlistFollows { get; set; }


        //Tales
        public DbSet<TaleHistory> TaleHistories { get; set; }

        public DbSet<TaleComment> TaleComments { get; set; }

        public DbSet<TaleDetail> TaleDetails { get; set; }

        public DbSet<TaleFlag> TaleFlags { get; set; }

        public DbSet<TaleFollow> TaleFollows { get; set; }

        public DbSet<TaleDraft> TaleDrafts { get; set; }

        public DbSet<TaleBasic> TaleBasics { get; set; }

        public DbSet<TaleList> TaleLists { get; set; }

        public DbSet<TaleRating> TaleRatings { get; set; }

        public DbSet<TaleShare> TaleShares { get; set; }


        //Identity
        public DbSet<AccountAdmin> AccountAdmins { get; set; }

        public DbSet<AccountBasic> AccountBasics { get; set; }

        public DbSet<AccountContact> AccountContacts { get; set; }

        public DbSet<AccountDetail> AccountDetails { get; set; }

        public DbSet<AccountNotification> AccountNotifications { get; set; }

        public DbSet<AccountTeam> AccountTeams { get; set; }

        public DbSet<AccountWriter> AccountWriters { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("reads"); // Default schema for this context

            // Configurations 

            //Tagging
            modelBuilder.ApplyConfiguration(new TagConfiguration());

            //Analysis
            modelBuilder.ApplyConfiguration(new InsightAddendumConfiguration());

            modelBuilder.ApplyConfiguration(new InsightCommentConfiguration());

            modelBuilder.ApplyConfiguration(new InsightFlagConfiguration());

            modelBuilder.ApplyConfiguration(new InsightFollowConfiguration());

            modelBuilder.ApplyConfiguration(new InsightDetailConfiguration());

            modelBuilder.ApplyConfiguration(new InsightListConfiguration());

            modelBuilder.ApplyConfiguration(new InsightRatingConfiguration());

            modelBuilder.ApplyConfiguration(new InsightShareConfiguration());

            //Discovery
            modelBuilder.ApplyConfiguration(new WatchlistCommentConfiguration());

            modelBuilder.ApplyConfiguration(new WatchlistDetailConfiguration());

            modelBuilder.ApplyConfiguration(new WatchlistLinkedTaleConfiguration());

            modelBuilder.ApplyConfiguration(new WatchlistFlagConfiguration());

            modelBuilder.ApplyConfiguration(new WatchlistFollowConfiguration());

            modelBuilder.ApplyConfiguration(new WatchlistRatingConfiguration());

            //Publishing
            modelBuilder.ApplyConfiguration(new TaleHistoryConfiguration());

            modelBuilder.ApplyConfiguration(new TaleCommentConfiguration());

            modelBuilder.ApplyConfiguration(new TaleFlagConfiguration());

            modelBuilder.ApplyConfiguration(new TaleFollowConfiguration());

            modelBuilder.ApplyConfiguration(new TaleListConfiguration());

            modelBuilder.ApplyConfiguration(new TaleDetailConfiguration());

            modelBuilder.ApplyConfiguration(new TaleDraftConfiguration());

            modelBuilder.ApplyConfiguration(new TaleBasicConfiguration());

            modelBuilder.ApplyConfiguration(new TaleRatingConfiguration());

            modelBuilder.ApplyConfiguration(new TaleShareConfiguration());


            //Account
            modelBuilder.ApplyConfiguration(new AccountAdminConfiguration());

            modelBuilder.ApplyConfiguration(new AccountBasicConfiguration());

            modelBuilder.ApplyConfiguration(new AccountContactConfiguration());

            modelBuilder.ApplyConfiguration(new AccountDetailConfiguration());

            modelBuilder.ApplyConfiguration(new AccountNotificationConfiguration());

            modelBuilder.ApplyConfiguration(new AccountTeamConfiguration());

            modelBuilder.ApplyConfiguration(new AccountWriterConfiguration());

        }
    }

}
