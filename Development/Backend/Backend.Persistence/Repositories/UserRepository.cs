using Backend.Application.Features.UserManagement.Commands.LoginUser;
using Backend.Application.Features.UserManagement.Common;
using Backend.Application.Features.UserManagement.Queries.LoadActivities;
using Backend.Application.Features.UserManagement.Queries.LoadAdmins;
using Backend.Application.Features.UserManagement.Queries.LoadUser;
using Backend.Application.Features.UserManagement.Queries.LoadUsernameProfile;
using Backend.Application.Features.UserManagement.Queries.LoadUserProfile;
using Backend.Application.Features.UserManagement.Queries.LoadWriters;
using Backend.Application.Repositories;
using Backend.Domain.Enums;
using Backend.Domain.Models.UserManagement.Entities;
using Backend.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Persistence.Repositories
{
    public class UserRepository(BackendDbContext dbContext)
        : IUserRepository
    {

        private readonly BackendDbContext _dbContext = dbContext;

        public async Task<bool> CheckIfEmailAddressExists(string emailAddress)
        {
            return await (from account in _dbContext.Accounts
                          where account.EmailAddress!.Value.ToLower() == emailAddress.ToLower()
                          select account)
                       .AnyAsync();
        }

        public async Task<bool> CheckIfPhoneNumberExists(string phoneNumber)
        {
            return await (from account in _dbContext.Accounts
                          where account.PhoneNumber!.Value.ToLower() == phoneNumber.ToLower()
                          select account)
                       .AnyAsync();
        }

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            return await(from account in _dbContext.Accounts
                         where account.Username.Value.ToLower() == username.ToLower()
                         select account)
               .AnyAsync();
        }

        public async Task<Account?> GetAccountById(Guid accountId)
        {
            return await (from account in _dbContext.Accounts
                          where account.Id == accountId
                          select account)
                          .Include(c => c.Profile)
                          .Include(c => c.Admin)
                         .Include(c => c.Contacts)
                         .Include(c => c.Activities)
               .SingleOrDefaultAsync();
        }

        public async Task<List<MailingList>?> GetMailingList(Guid accountId)
        {
            return await (from follower in _dbContext.AccountFollowers
                          where follower.AccountId == accountId
                          join account in _dbContext.Accounts
                          on follower.FollowerId equals account.Id
                        
                          select new MailingList()
                          {
                              Username = account.Username.Value,
                              EmailAddress = account.EmailAddress!.Value,
                          })

               .ToListAsync();
        }

        public async Task<LoginUserResponse?> LoadLoginUserResponse(Guid accountId)
        {

            return await (from account in _dbContext.Accounts
                          where account.Id == accountId
                          join profile in _dbContext.Profiles
                          on account.Id equals profile.AccountId

                          select new LoginUserResponse()
                          {
                              Username = account.Username.Value,
                              DisplayPhoto = profile.PhotoUrl!.Value,
                              Title = profile.Title.Value,
                              Bio = profile.Bio.Value,
                              Role = account.Role,
                              EmailAddress = account.EmailAddress!.Value,
                              PhoneNumber = account.PhoneNumber!.Value,
                              RegisterDate = account.RegisteredDate,
                              IsHidden = profile.IsHidden,
                              Followers = account.Followers.Count,
                              ProfileViews = account.Views,
                              Contacts = (from contact in _dbContext.Contacts
                                        where contact.AccountId == accountId
                                        select new Contacts()
                                        {
                                            Type = contact.Type,
                                            Text = contact.Text.Value
                                        }).ToList(),
                          })

              .SingleOrDefaultAsync();
        }

        public async Task<List<WriterSummary>> LoadMostPopularWriters(Guid accountId)
        {

            return await (from admin in _dbContext.Admins
                          join account in _dbContext.Accounts
                          on admin.AccountId equals account.Id
                          where account.Role != RoleTypes.None
                          join profile in _dbContext.Profiles
                           on account.Id equals profile.AccountId

                          select new WriterSummary()
                          {
                              Id = account.Id,
                              Username = account.Username.Value,
                              RawDisplayPhoto = profile.PhotoUrl!.Value,
                              WriterDate = admin.Date,
                              Country = admin.Country,
                              IsHidden = profile.IsHidden,
                              IsFollowingUser = account.Followers.Any(c => c.FollowerId == accountId),
                              Followers = account.Followers.Count,
                              Tales = (from tale in _dbContext.Tales
                                       where tale.CreatorId == account.Id && tale.Status == TaleStatuses.Published
                                       select tale).Count()
                          }).OrderByDescending(c => c.Tales).Take(4).ToListAsync();
        }

        public async Task<List<WriterSummary>> LoadMostRecentWriters(Guid accountId)
        {

            return await (from admin in _dbContext.Admins
                          join account in _dbContext.Accounts
                          on admin.AccountId equals account.Id
                          where account.Role != RoleTypes.None
                          join profile in _dbContext.Profiles
                          on account.Id equals profile.AccountId

                          select new WriterSummary()
                          {
                              Id = account.Id,
                              Username = account.Username.Value,
                              RawDisplayPhoto = profile.PhotoUrl!.Value,
                              WriterDate = admin.Date,
                              Country = admin.Country,
                              IsHidden = profile.IsHidden,
                              IsFollowingUser = account.Followers.Any(c => c.FollowerId == accountId),
                              Followers = account.Followers.Count,
                              Tales = (from tale in _dbContext.Tales
                                       where tale.CreatorId == account.Id && tale.Status == TaleStatuses.Published
                                       select tale).Count()
                          }).OrderByDescending(c => c.WriterDate).Take(4).ToListAsync();
        }


        public async Task<LoadUserProfileQueryResponse?> LoadUserProfile(Guid id, Guid accountId)
        {

            return await (from account in _dbContext.Accounts
                          where account.Id == id
                          join profile in _dbContext.Profiles
                          on account.Id equals profile.AccountId

                          select new LoadUserProfileQueryResponse()
                          {
                              Username = account.Username.Value,
                              RawDisplayPhoto = profile.PhotoUrl!.Value,
                              RawTitle = profile.Title.Value,
                              RawBio = profile.Bio.Value,
                              Id = account.Id,
                              RegisterDate = account.RegisteredDate,
                              IsHidden = profile.IsHidden,
                              IsFollowingUser = account.Followers.Any(c => c.FollowerId == accountId),
                              Followers = account.Followers.Count,
                              ProfileViews = account.Views,
                              RawContacts = (from contact in _dbContext.Contacts
                                          where contact.AccountId == account.Id
                                          select new Contacts()
                                          {
                                              Type = contact.Type,
                                              Text = contact.Text.Value
                                          }).ToList(),
                              Threads = (from thread in _dbContext.Threads
                                         where thread.ThreaderId == account.Id && thread.IsOnline == true
                                         select thread).Count(),
                              Tales = (from tale in _dbContext.Tales
                                         where tale.CreatorId == account.Id && tale.Status == TaleStatuses.Published
                                         select tale).Count()
                          })

              .SingleOrDefaultAsync();
        }

        public async Task<LoadUsernameProfileQueryResponse?> LoadUsernameProfile(string id, Guid accountId)
        {

            return await (from account in _dbContext.Accounts
                          where account.Username.Value.ToLower() == id.ToLower()
                          join profile in _dbContext.Profiles
                          on account.Id equals profile.AccountId

                          select new LoadUsernameProfileQueryResponse()
                          {
                              Username = account.Username.Value,
                              RawDisplayPhoto = profile.PhotoUrl!.Value,
                              RawTitle = profile.Title.Value,
                              RawBio = profile.Bio.Value,
                              Id = account.Id,
                              RegisterDate = account.RegisteredDate,
                              IsHidden = profile.IsHidden,
                              IsFollowingUser = account.Followers.Any(c => c.FollowerId == accountId),
                              Followers = account.Followers.Count,
                              ProfileViews = account.Views,
                              RawContacts = (from contact in _dbContext.Contacts
                                             where contact.AccountId == account.Id
                                             select new Contacts()
                                             {
                                                 Type = contact.Type,
                                                 Text = contact.Text.Value
                                             }).ToList(),
                              Threads = (from thread in _dbContext.Threads
                                         where thread.ThreaderId == account.Id && thread.IsOnline == true
                                         select thread).Count(),
                              Tales = (from tale in _dbContext.Tales
                                       where tale.CreatorId == account.Id && tale.Status == TaleStatuses.Published
                                       select tale).Count()
                          })

              .SingleOrDefaultAsync();
        }

        public async Task<LoadUserQueryResponse?> LoadUser(string id, Guid accountId)
        {

            return await (from account in _dbContext.Accounts
                          where account.Username.Value.ToLower() == id.ToLower()
                          join profile in _dbContext.Profiles
                          on account.Id equals profile.AccountId

                          select new LoadUserQueryResponse()
                          {
                              Username = account.Username.Value,
                              RawDisplayPhoto = profile.PhotoUrl!.Value,
                              RawTitle = profile.Title.Value,
                              RawBio = profile.Bio.Value,
                              Id = account.Id,
                              RegisterDate = account.RegisteredDate,
                              IsHidden = profile.IsHidden,
                              IsFollowingUser = account.Followers.Any(c => c.FollowerId == accountId),
                              Followers = account.Followers.Count,
                              ProfileViews = account.Views,
                              RawContacts = (from contact in _dbContext.Contacts
                                             where contact.AccountId == account.Id
                                             select new Contacts()
                                             {
                                                 Type = contact.Type,
                                                 Text = contact.Text.Value
                                             }).ToList(),
                              Threads = (from thread in _dbContext.Threads
                                         where thread.ThreaderId == account.Id && thread.IsOnline == true
                                         select thread).Count(),
                              Tales = (from tale in _dbContext.Tales
                                       where tale.CreatorId == account.Id && tale.Status == TaleStatuses.Published
                                       select tale).Count()
                          })

              .SingleOrDefaultAsync();
        }

        public async Task<string?> GetUsernameById(Guid userId)
        {
            return await (from account in _dbContext.Accounts
                          where account.Id == userId
                          select account.Username.Value)
                    .AsNoTracking()
                   .SingleOrDefaultAsync();
        }
        
        public async Task<Account?> GetAccountByUsername(string username)
        {
            return await (from account in _dbContext.Accounts
                          where account.Username.Value.ToLower() == username.ToLower()
                          select account)
                          .Include(c => c.Profile)
                          .Include(c => c.Contacts)
                    .SingleOrDefaultAsync();
        }

        public async Task<Account?> GetValidAccountFromRefreshToken(string refreshToken)
        {
            return await (from account in _dbContext.Accounts
                          where account.RefreshToken != null &&
                          account.RefreshToken.Value == refreshToken &&
                          account.RefreshToken.ExpiryDate >= DateTime.UtcNow
                          select account)
                               .SingleOrDefaultAsync();
        }

        public async Task<Account?> GetUserFromRefreshToken(string token)
        {
            return await (from account in _dbContext.Accounts
                          where account.RefreshToken!.Value == token
                         
                          select account)
                          .SingleOrDefaultAsync();
        }

        public async Task<LoadAdminsQueryResponse> LoadAllAdmins(RoleTypes? role,
          Countries? country, SortTypes? sort, string? username, int pointer,
         int size)
        {

            //Get admins
            IQueryable<AdminSummary> admins = LoadAdminsIQueryable();

            if (role != null)
                admins = admins.Where(c => c.Role == role);

            if (country != null)
                admins = admins.Where(c => c.Country == country);

            if (username != null)
                admins = admins.Where(c => c.Username.ToLower() == username.ToLower());

            //Sort functions
            admins = SortAdmins(sort, admins);

            //Return functions
            return new LoadAdminsQueryResponse()
            {
                Admins = await admins.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await admins.CountAsync()
            };
        }

        public async Task<LoadWritersQueryResponse> LoadAllWriters(Guid accountId,
        Countries? country, SortTypes? sort, string? username, int pointer,
       int size)
        {

            //Get writers
            IQueryable<WriterSummary> writers = LoadWritersIQueryable(accountId);

            if (country != null)
                writers = writers.Where(c => c.Country == country);

            if (username != null)
                writers = writers.Where(c => c.Username.ToLower() == username.ToLower());

            //Sort functions
            writers = SortWriters(sort, writers);

            //Return functions
            return new LoadWritersQueryResponse()
            {
                Writers = await writers.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await writers.CountAsync()
            };
        }

        public async Task<LoadActivitiesQueryResponse> LoadActivities(Guid accountId, ActivityTypes? type,
        bool? hasRead, string? keyword, SortTypes? sort, int pointer, int size)
        {

            //Get admins
            IQueryable<ActivitySummary> activities = LoadActivitiesIQueryable(accountId, keyword);

            if (type != null)
                activities = activities.Where(c => c.Type == type);

            if (hasRead != null)
                activities = activities.Where(c => c.HasRead == hasRead);

            //Sort functions
            activities = SortActivities(sort, activities);

            //Return functions
            return new LoadActivitiesQueryResponse()
            {
                Activities = await activities.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await activities.CountAsync()
            };
        }

        #region Helpers

        private IQueryable<AdminSummary> LoadAdminsIQueryable()
        {
            
            return from admin in _dbContext.Admins
                   join account in _dbContext.Accounts
                   on admin.AccountId equals account.Id

                   select new AdminSummary()
                   {
                       Id = account.Id,
                       Username = account.Username.Value,
                       Role = account.Role,
                       Country = admin.Country,
                       Address = admin.Address.Value,
                       ApplicationDate = admin.Date,
                       ApplicationUrl = admin.Application.Value,

                   };
        }

        private static IQueryable<AdminSummary> SortAdmins(SortTypes? sort, IQueryable<AdminSummary> admins)
        {
            return sort switch
            {
                SortTypes.Most_Recent => admins.OrderByDescending(s => s.ApplicationDate),
                SortTypes.Least_Recent => admins.OrderBy(s => s.ApplicationDate),
                _ => admins.OrderByDescending(s => s.ApplicationDate)
            };
        }

        private IQueryable<ActivitySummary> LoadActivitiesIQueryable(Guid accountId, string? keyword)
        {

            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from activity in _dbContext.Activities
            where activity.AccountId == accountId

            where keywords != null && (keywords.Any(k => activity.Details.Value.ToLower().Contains(k.ToLower())))

                   select new ActivitySummary()
                   {
                       Id = activity.Id,
                       Type = activity.Type,
                       ConstructorType = activity.ConstructorType,
                       Date = activity.ActiveDate,
                       Details = activity.Details.Value,
                       HasRead = activity.HasRead
                   };
        }

        private static IQueryable<ActivitySummary> SortActivities(SortTypes? sort, IQueryable<ActivitySummary> activities)
        {
            return sort switch
            {
                SortTypes.Most_Recent => activities.OrderByDescending(s => s.Date),
                SortTypes.Least_Recent => activities.OrderBy(s => s.Date),
                _ => activities.OrderByDescending(s => s.Date)
            };
        }

        private IQueryable<WriterSummary> LoadWritersIQueryable(Guid accountId)
        {

            return from admin in _dbContext.Admins
                   join account in _dbContext.Accounts
                   on admin.AccountId equals account.Id
                   where account.Role != RoleTypes.None
                   join profile in _dbContext.Profiles
                   on account.Id equals profile.AccountId

                   select new WriterSummary()
                   {
                       Id = account.Id,
                       Username = account.Username.Value,
                       RawDisplayPhoto = profile.PhotoUrl!.Value,
                       WriterDate = admin.Date,
                       Country = admin.Country,
                       IsHidden = profile.IsHidden,
                       IsFollowingUser = account.Followers.Any(c => c.FollowerId == accountId),
                       Followers = account.Followers.Count,
                       Tales = (from tale in _dbContext.Tales
                                where tale.CreatorId == account.Id && tale.Status == TaleStatuses.Published
                                select tale).Count()
                   };
        }

        private static IQueryable<WriterSummary> SortWriters(SortTypes? sort, IQueryable<WriterSummary> writers)
        {
            return sort switch
            {
                SortTypes.Most_Recent => writers.OrderByDescending(s => s.WriterDate),
                SortTypes.Least_Recent => writers.OrderBy(s => s.WriterDate),
                _ => writers.OrderByDescending(s => s.WriterDate)
            };
        }

        #endregion

    }
}
