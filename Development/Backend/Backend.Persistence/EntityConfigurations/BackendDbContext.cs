using Backend.Domain.Models.TalesManagement.Entities;
using Backend.Domain.Models.TempUserManagement.Entities;
using Backend.Domain.Models.ThreadsManagement;
using Backend.Domain.Models.UserManagement.Entities;
using Backend.Domain.Models.WatchListManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.EntityConfigurations
{
    public class BackendDbContext(DbContextOptions<BackendDbContext> options) 
        : DbContext(options)
    {

        #region DBSets

        #region TalesManagement

        public DbSet<Tale> Tales { get; set; }

        public DbSet<TaleComment> TaleComments { get; set; }

        public DbSet<TaleCommentFlag> TaleCommentFlags { get; set; }

        public DbSet<TaleCommentRating> TaleCommentRatings { get; set; }

        public DbSet<TaleFlag> TaleFlags { get; set; }

        public DbSet<TaleFollower> TaleFollowers { get; set; }

        public DbSet<TaleHistory> TaleHistories { get; set; }

        public DbSet<TaleRating> TaleRatings { get; set; }

        public DbSet<TaleShare> TaleShares { get; set; }

        public DbSet<TaleTag> TaleTags { get; set; }

        #endregion

        #region TempUserManagement

        public DbSet<TempUser> TempUsers { get; set; }

        #endregion

        #region ThreadsManagement

        public DbSet<Threads> Threads { get; set; }

        public DbSet<ThreadsAddendum> ThreadsAddendums { get; set; }

        public DbSet<ThreadsComment> ThreadsComments { get; set; }

        public DbSet<ThreadsCommentFlag> ThreadsCommentFlags { get; set; }

        public DbSet<ThreadsCommentRating> ThreadsCommentRatings { get; set; }

        public DbSet<ThreadsFlag> ThreadsFlags { get; set; }

        public DbSet<ThreadsFollower> ThreadsFollowers { get; set; }

        public DbSet<ThreadsRating> ThreadsRatings { get; set; }

        public DbSet<ThreadsShare> ThreadsShares { get; set; }

        public DbSet<ThreadsTag> ThreadTags { get; set; }

        #endregion

        #region UserManagement

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountFollower> AccountFollowers { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<LoginHistory> LoginHistories { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        #endregion

        #region WatchListManagement

        public DbSet<LinkedTale> LinkedTales { get; set; }

        public DbSet<WatchList> WatchLists { get; set; }

        public DbSet<WatchListFollower> WatchListFollowers { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(BackendDbContext).Assembly);
        }

    }

}
