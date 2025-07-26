using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Application.Features.TalesManagement.Queries.LoadTale;
using OutScribed.Application.Features.TalesManagement.Queries.LoadTaleComments;
using OutScribed.Application.Features.TalesManagement.Queries.LoadTaleResponses;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.TalesManagement.Entities;
using OutScribed.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using OutScribed.Application.Features.TalesManagement.Queries.LoadAllTales;
using OutScribed.Application.Features.TalesManagement.Queries.LoadTaleLinks;
using OutScribed.Application.Features.TalesManagement.Queries.LoadSelfTaleDrafts;
using OutScribed.Application.Features.TalesManagement.Queries.LoadAllTaleDrafts;

namespace OutScribed.Persistence.Repositories
{

    public class TaleRepository(OutScribedDbContext dbContext)
    : ITaleRepository
    {

        private readonly OutScribedDbContext _dbContext = dbContext;

        public async Task<Tale?> GetTaleById(Guid taleId)
        {
            return await (from tale in _dbContext.Tales
                          where tale.Id == taleId
                          select tale)
                          .Include(c => c.Ratings)
                          .Include(c => c.Comments)
                          .Include(c => c.Flags)
                          .Include(c => c.Followers)
               .SingleOrDefaultAsync();
        }

        public async Task<TaleDraftSummary?> LoadCreateTaleResponse(Guid taleId)
        {

            return await (from tale in _dbContext.Tales
                          where tale.Id == taleId
                          select new TaleDraftSummary()
                          {
                              Id = tale.Id,
                              Title = tale.Title.Value,
                              Status = tale.Status,
                              Category = tale.Category,
                              Date = tale.CreationDate
                          }).SingleOrDefaultAsync();
        }

        public async Task<List<string>?> GetTaleTags(Guid taleId)
        {
            return await (from tag in _dbContext.TaleTags
                          where tag.TaleId == taleId
                          select tag.Title.Value).ToListAsync();
        }

        public async Task<TaleHeaderSummary?> LoadTaleHeaderSummary(Guid accountId, Guid taleId)
        {

            return await (from tale in _dbContext.Tales
                          where tale.Id == taleId
                          join account in _dbContext.Accounts
                          on tale.CreatorId equals account.Id
                          select new TaleHeaderSummary()
                          {
                              Id = tale.Id,
                              Title = tale.Title.Value,
                              TaleUrl = tale.Url!.Value,
                              Category = tale.Category,
                              Country = tale.Country,
                              Summary = tale.Summary!.Value,
                              Date = tale.CreationDate,
                              WriterId = account.Id,
                              WriterUsername = account.Username.Value,
                              IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId)
                          }).SingleOrDefaultAsync();
        }

        public async Task<TaleBrief?> GetTaleBrief(Guid taleId)
        {
            return await (from tale in _dbContext.Tales
                          where tale.Id == taleId
                          select new TaleBrief()
                          {
                              Title = tale.Title.Value,
                              TaleUrl = tale.Url!.Value
                          })

               .SingleOrDefaultAsync();
        }

        public async Task<LoadAllTalesQueryResponse> LoadAllTales(Guid accountId, Categories? category,
            Countries? country, string? username, SortTypes? sort, Guid? watchlistId, string? tag, string? keyword, int pointer, int size)
        {

            //Get tales
            IQueryable<TaleSummary> tales = tag != null ? LoadAllTalesByTagsIQueryable(tag, accountId)
                : watchlistId != null ? LoadAllTalesByWatchlistIQueryable(watchlistId.Value, accountId)
                : LoadAllTalesIQueryable(keyword, accountId);

            if (category != null)
                tales = tales.Where(c => c.Category == category);

            if (country != null)
                tales = tales.Where(c => c.Country == country);

            if (username != null)
                tales = tales.Where(c => c.WriterUsername.ToLower() == username.Trim('@').ToLower());

            //Sort functions
            tales = SortTales(sort, tales);

            //Return functions
            return new LoadAllTalesQueryResponse()
            {
                Tales = await tales.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await tales.CountAsync()
            };
        }

        public async Task<LoadTaleQueryResponse?> LoadTale(string url, Guid accountId)
        {

            return await (from tale in _dbContext.Tales
                          where tale.Url!.Value == url

                          join account in _dbContext.Accounts
                     on tale.CreatorId equals account.Id

                          select new LoadTaleQueryResponse()
                          {
                              Id = tale.Id,
                              Title = tale.Title.Value,
                              TaleUrl = tale.Url!.Value,
                              Category = tale.Category,
                              Country = tale.Country,
                              Summary = tale.Summary!.Value,
                              PhotoUrl = tale.PhotoUrl!.Value,
                              Details = tale.Details!.Value,
                              Date = tale.CreationDate,
                              Views = tale.Views,
                              Flags = tale.Flags.Count,
                              Likes = tale.Ratings.Count(c => c.Type == RateTypes.Like),
                              Hates = tale.Ratings.Count(c => c.Type == RateTypes.Hate),
                              HasFlagged = tale.Flags.Any(c => c.FlaggerId == accountId),
                              Shares = tale.Sharers.Count,
                              CommentsCount = tale.Comments.Count,
                              Followers = tale.Followers.Count,
                              WriterId = account.Id,
                              WriterProfileViews = account.Views,
                              WriterFollowers = account.Followers.Count,
                              IsFollowingWriter = account.Followers.Any(c => c.FollowerId == accountId),
                              WriterUsername = account.Username.Value,
                              IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId),
                              MyRatings = tale.Ratings
                                               .Where(c => c.RaterId == accountId)
                                               .Select(c => c.Type)
                                               .SingleOrDefault(),

                              Threads = (from thread in _dbContext.Threads
                                         where thread.TaleId == tale.Id && thread.IsOnline == true
                                         select thread).Count(),
                              Tags = (from tag in _dbContext.TaleTags
                                      where tag.TaleId == tale.Id
                                      select tag.Title.Value).ToList()
                          }).SingleOrDefaultAsync();
        }

        public async Task<TaleCommentSummary?> LoadTaleComment(Guid accountId, Guid commentId)
        {

            return await (from comment in _dbContext.TaleComments
                          where comment.Id == commentId
                          join account in _dbContext.Accounts
                          on comment.CommentatorId equals account.Id
                          join profile in _dbContext.Profiles
                           on account.Id equals profile.AccountId
                          select new TaleCommentSummary()
                          {
                              Id = comment.Id,
                              Details = comment.Details.Value,
                              TaleId = comment.TaleId,
                              ParentId = comment.ParentId,
                              Date = comment.Date,
                              ResponsesCount = comment.Responses.Count,
                              Likes = comment.Ratings
                                                                        .Count(c => c.Type == RateTypes.Like),
                              Hates = comment.Ratings
                                                                        .Count(c => c.Type == RateTypes.Hate),
                              MyRatings = comment.Ratings
                                                                        .Where(c => c.RaterId == accountId)
                                                                        .Select(c => c.Type)
                                                                        .FirstOrDefault(),
                              CommentatorId = account.Id,
                              CommentatorDisplayPhoto = profile.PhotoUrl!.Value,
                              CommentatorUsername = account.Username.Value,
                              Flags = comment.Flags.Count,
                              HasFlagged = comment.Flags
                                                        .Any(c => c.FlaggerId == accountId)

                          }).SingleOrDefaultAsync();
        }

        public async Task<LoadTaleCommentsQueryResponse> LoadTaleComments(Guid accountId, Guid taleId,
            string? username, string? keyword, SortTypes? sort, int pointer, int size)
        {

            //Get posts
            IQueryable<TaleCommentSummary> comments = LoadCommentSummaryIQueryable(taleId, accountId, keyword);

            if (username != null)
                comments = comments.Where(c => c.CommentatorUsername.ToLower() == username.Trim('@').ToLower());

            //Sort functions
            comments = SortComments(sort, comments);

            //Return functions
            return new LoadTaleCommentsQueryResponse()
            {
                Comments = await comments.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await comments.CountAsync()
            };
        }

        public async Task<LoadTaleResponsesQueryResponse> LoadTaleResponses(Guid accountId, Guid parentId,
         string? username, string? keyword, SortTypes? sort, int pointer, int size)
        {

            //Get posts
            IQueryable<TaleCommentSummary> responses = LoadResponseSummaryIQueryable(parentId, accountId, keyword);

            if (username != null)
                responses = responses.Where(c => c.CommentatorUsername.ToLower() == username.Trim('@').ToLower());

            //Sort functions
            responses = SortComments(sort, responses);

            //Return functions
            return new LoadTaleResponsesQueryResponse()
            {
                Responses = await responses.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await responses.CountAsync()
            };
        }

        public async Task<List<TaleSummary>> LoadMostRecentTales(Guid accountId)
        {

            return await (from tale in _dbContext.Tales
                          where tale.Status == TaleStatuses.Published
                          join account in _dbContext.Accounts
                        on tale.CreatorId equals account.Id
                          select new TaleSummary()
                          {
                              Id = tale.Id,
                              Title = tale.Title.Value,
                              Category = tale.Category,
                              Country = tale.Country,
                              Summary = tale.Summary!.Value,
                              PhotoUrl = tale.PhotoUrl!.Value,
                              Date = tale.CreationDate,
                              Views = tale.Views,
                              Likes = tale.Ratings
                                                                               .Count(c => c.Type == RateTypes.Like),
                              Hates = tale.Ratings
                                                                               .Count(c => c.Type == RateTypes.Hate),
                              Shares = tale.Sharers.Count,
                              Comments = tale.Comments.Count,
                              Followers = tale.Followers.Count,
                              TaleUrl = tale.Url!.Value,
                              IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId),
                              WriterId = account.Id,
                              WriterUsername = account.Username.Value,
                              Threads = (from thread in _dbContext.Threads
                                         where thread.TaleId == tale.Id
                                         select thread).Count()
                          }).OrderByDescending(c => c.Date).Take(3).ToListAsync();
        }

        public async Task<List<TaleSummary>> LoadMostPopularTales(Guid accountId)
        {

            return await (from tale in _dbContext.Tales
                          where tale.Status == TaleStatuses.Published
                          join account in _dbContext.Accounts
                        on tale.CreatorId equals account.Id

                          select new TaleSummary()
                          {
                              Id = tale.Id,
                              Title = tale.Title.Value,
                              Category = tale.Category,
                              Country = tale.Country,
                              Summary = tale.Summary!.Value,
                              PhotoUrl = tale.PhotoUrl!.Value,
                              Date = tale.CreationDate,
                              Views = tale.Views,
                              Likes = tale.Ratings
                                                                         .Count(c => c.Type == RateTypes.Like),
                              Hates = tale.Ratings
                                                                         .Count(c => c.Type == RateTypes.Hate),
                              Shares = tale.Sharers.Count,
                              Comments = tale.Comments.Count,
                              Followers = tale.Followers.Count,
                              TaleUrl = tale.Url!.Value,
                              IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId),
                              WriterId = account.Id,
                              WriterUsername = account.Username.Value,
                              Threads = (from thread in _dbContext.Threads
                                         where thread.TaleId == tale.Id
                                         select thread).Count()
                          }).OrderByDescending(c => c.Views).Take(4).ToListAsync();
        }

        public async Task<LoadTaleLinksQueryResponse> LoadTaleLinks(Guid userId,
         SortTypes? sort, string? keyword, int pointer, int size)
        {

            //Get tales
            IQueryable<TaleLink> tales = LoadTaleLinksIQueryable(userId, keyword);

            //Sort functions
            tales = SortTaleLinks(sort, tales);

            //Return functions
            return new LoadTaleLinksQueryResponse()
            {
                Tales = await tales.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await tales.CountAsync()
            };
        }

        public async Task<LoadSelfTaleDraftsQueryResponse> LoadSelfTaleDrafts(Guid userId,
           TaleStatuses? status, Categories? category, Countries? country, SortTypes? sort,
           string? keyword, int pointer, int size)
        {

            //Get tales
            IQueryable<TaleDraftSummary> tales = LoadSelfDraftTalesIQueryable(userId, keyword);

            if (status != null)
                tales = tales.Where(c => c.Status == status);

            if (category != null)
                tales = tales.Where(c => c.Category == category);

            if (country != null)
                tales = tales.Where(c => c.Country == country);

            //Sort functions
            tales = SortTales(sort, tales);

            //Return functions
            return new LoadSelfTaleDraftsQueryResponse()
            {
                Tales = await tales.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await tales.CountAsync()
            };
        }

        public async Task<LoadAllTaleDraftsQueryResponse> LoadAllTaleDrafts(TaleStatuses? status,
            Categories? category, Countries? country, string? username, SortTypes? sort, string? keyword, int pointer,
           int size)
        {

            //Get tales
            IQueryable<TaleDraftSummary> tales = LoadAllDraftTalesIQueryable(keyword);

            //Sort functions
            tales = SortTales(sort, tales);

            if (status != null)
                tales = tales.Where(c => c.Status == status);

            if (category != null)
                tales = tales.Where(c => c.Category == category);

            if (country != null)
                tales = tales.Where(c => c.Country == country);

            if (username != null)
                tales = tales.Where(c => c.WriterUsername.ToLower() == username.Trim('@').ToLower());

            //Return functions
            return new LoadAllTaleDraftsQueryResponse()
            {
                Tales = await tales.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await tales.CountAsync()
            };
        }

        #region Helpers

        private IQueryable<TaleSummary> LoadAllTalesIQueryable(string? keyword, Guid accountId)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from tale in _dbContext.Tales
                   where tale.Status == TaleStatuses.Published
                   join account in _dbContext.Accounts
                 on tale.CreatorId equals account.Id

                   where keywords != null && (keywords.Any(k => tale.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new TaleSummary()
                   {
                       Id = tale.Id,
                       Title = tale.Title.Value,
                       Category = tale.Category,
                       Country = tale.Country,
                       Summary = tale.Summary!.Value,
                       PhotoUrl = tale.PhotoUrl!.Value,
                       Date = tale.CreationDate,
                       Views = tale.Views,
                       Likes = tale.Ratings
                                                                        .Count(c => c.Type == RateTypes.Like),
                       Hates = tale.Ratings
                                                                        .Count(c => c.Type == RateTypes.Hate),
                       Shares = tale.Sharers.Count,
                       Comments = tale.Comments.Count,
                       Followers = tale.Followers.Count,
                       TaleUrl = tale.Url!.Value,
                       IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId),
                       WriterId = account.Id,
                       WriterUsername = account.Username.Value,
                       Threads = (from thread in _dbContext.Threads
                                  where thread.TaleId == tale.Id
                                  select thread).Count()
                   };
        }

        private IQueryable<TaleSummary> LoadAllTalesByTagsIQueryable(string tag, Guid accountId)
        {

            return from taleTag in _dbContext.TaleTags
                   where taleTag.Title.Value.ToLower() == tag.ToLower()
                   join tale in _dbContext.Tales
                   on taleTag.TaleId equals tale.Id
                   where tale.Status == TaleStatuses.Published
                   join account in _dbContext.Accounts
                 on tale.CreatorId equals account.Id

                   select new TaleSummary()
                   {
                       Id = tale.Id,
                       Title = tale.Title.Value,
                       Category = tale.Category,
                       Country = tale.Country,
                       Summary = tale.Summary!.Value,
                       PhotoUrl = tale.PhotoUrl!.Value,
                       Date = tale.CreationDate,
                       Views = tale.Views,
                       Likes = tale.Ratings
                                                                        .Count(c => c.Type == RateTypes.Like),
                       Hates = tale.Ratings
                                                                        .Count(c => c.Type == RateTypes.Hate),
                       Shares = tale.Sharers.Count,
                       Comments = tale.Comments.Count,
                       Followers = tale.Followers.Count,
                       TaleUrl = tale.Url!.Value,
                       IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId),
                       WriterId = account.Id,
                       WriterUsername = account.Username.Value,
                       Threads = (from thread in _dbContext.Threads
                                  where thread.TaleId == tale.Id
                                  select thread).Count()
                   };
        }

        private IQueryable<TaleSummary> LoadAllTalesByWatchlistIQueryable(Guid watchlistId, Guid accountId)
        {

            return from watchlist in _dbContext.LinkedTales
                   where watchlist.WatchListId == watchlistId
                   join tale in _dbContext.Tales
                   on watchlist.TaleId equals tale.Id
                   where tale.Status == TaleStatuses.Published
                   join account in _dbContext.Accounts
                 on tale.CreatorId equals account.Id

                   select new TaleSummary()
                   {
                       Id = tale.Id,
                       Title = tale.Title.Value,
                       Category = tale.Category,
                       Country = tale.Country,
                       Summary = tale.Summary!.Value,
                       PhotoUrl = tale.PhotoUrl!.Value,
                       Date = tale.CreationDate,
                       Views = tale.Views,
                       Likes = tale.Ratings
                                                                        .Count(c => c.Type == RateTypes.Like),
                       Hates = tale.Ratings
                                                                        .Count(c => c.Type == RateTypes.Hate),
                       Shares = tale.Sharers.Count,
                       Comments = tale.Comments.Count,
                       Followers = tale.Followers.Count,
                       TaleUrl = tale.Url!.Value,
                       IsFollowingTale = tale.Followers.Any(c => c.FollowerId == accountId),
                       WriterId = account.Id,
                       WriterUsername = account.Username.Value,
                       Threads = (from thread in _dbContext.Threads
                                  where thread.TaleId == tale.Id
                                  select thread).Count()
                   };
        }

        private static IQueryable<TaleSummary> SortTales(SortTypes? sort, IQueryable<TaleSummary> tales)
        {
            return sort switch
            {
                SortTypes.Most_Recent => tales.OrderByDescending(s => s.Date),
                SortTypes.Most_Liked => tales.OrderByDescending(s => s.Likes),
                SortTypes.Least_Liked => tales.OrderByDescending(s => s.Hates),
                SortTypes.Most_Followed => tales.OrderByDescending(s => s.Followers),
                SortTypes.Most_Popular => tales.OrderByDescending(s => s.Views),
                SortTypes.Most_Commented => tales.OrderByDescending(s => s.Comments),
                SortTypes.Most_Shared => tales.OrderByDescending(s => s.Shares),
                SortTypes.Least_Recent => tales.OrderBy(s => s.Date),
                _ => tales.OrderByDescending(s => s.Date)
            };
        }

        private static IQueryable<TaleCommentSummary> SortComments(SortTypes? sort, IQueryable<TaleCommentSummary> comments)
        {
            return sort switch
            {
                SortTypes.Most_Liked => comments.OrderByDescending(s => s.Hates),
                SortTypes.Least_Liked => comments.OrderByDescending(s => s.Likes),
                SortTypes.Least_Recent => comments.OrderBy(s => s.Date),
                SortTypes.Most_Recent => comments.OrderByDescending(s => s.Date),
                _ => comments.OrderByDescending(s => s.Date),
            };
        }

        private IQueryable<TaleCommentSummary> LoadCommentSummaryIQueryable(Guid taleId, Guid accountId, string? keyword)
        {

            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from comment in _dbContext.TaleComments
                   where comment.TaleId == taleId && comment.ParentId == null
                   join account in _dbContext.Accounts
                   on comment.CommentatorId equals account.Id
                   join profile in _dbContext.Profiles
            on account.Id equals profile.AccountId

                   where keywords != null && keywords.Any(k => comment.Details.Value.ToLower().Contains(k.ToLower()))
                   select new TaleCommentSummary()
                   {
                       Id = comment.Id,
                       Details = comment.Details.Value,
                       TaleId = comment.TaleId,
                       Date = comment.Date,
                       ResponsesCount = comment.Responses.Count,
                       Likes = comment.Ratings
                                                                        .Count(c => c.Type == RateTypes.Like),
                       Hates = comment.Ratings
                                                                        .Count(c => c.Type == RateTypes.Hate),
                       MyRatings = comment.Ratings
                                                                        .Where(c => c.RaterId == accountId)
                                                                        .Select(c => c.Type)
                                                                        .SingleOrDefault(),
                       CommentatorId = account.Id,
                       CommentatorDisplayPhoto = profile.PhotoUrl!.Value,
                       CommentatorUsername = account.Username.Value,
                       Flags = comment.Flags.Count,
                       HasFlagged = comment.Flags
                                                        .Any(c => c.FlaggerId == accountId)

                   };
        }

        private IQueryable<TaleCommentSummary> LoadResponseSummaryIQueryable(Guid parentId, Guid accountId, string? keyword)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from comment in _dbContext.TaleComments
                   where comment.ParentId == parentId
                   join account in _dbContext.Accounts
                   on comment.CommentatorId equals account.Id
                   join profile in _dbContext.Profiles
            on account.Id equals profile.AccountId

                   where keywords != null && keywords.Any(k => comment.Details.Value.ToLower().Contains(k.ToLower()))

                   select new TaleCommentSummary()
                   {
                       Id = comment.Id,
                       Details = comment.Details.Value,
                       TaleId = comment.TaleId,
                       ParentId = comment.ParentId,
                       Date = comment.Date,
                       ResponsesCount = comment.Responses.Count,
                       Likes = comment.Ratings
                                                                         .Count(c => c.Type == RateTypes.Like),
                       Hates = comment.Ratings
                                                                         .Count(c => c.Type == RateTypes.Hate),
                       MyRatings = comment.Ratings
                                                                         .Where(c => c.RaterId == accountId)
                                                                         .Select(c => c.Type)
                                                                         .SingleOrDefault(),
                       CommentatorId = account.Id,
                       CommentatorDisplayPhoto = profile.PhotoUrl!.Value,
                       CommentatorUsername = account.Username.Value,
                       Flags = comment.Flags.Count,
                       HasFlagged = comment.Flags
                                                         .Any(c => c.FlaggerId == accountId)

                   };
        }


        private IQueryable<TaleDraftSummary> LoadSelfDraftTalesIQueryable(Guid userId, string? keyword)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from tale in _dbContext.Tales
                   where tale.CreatorId == userId

                   where keywords != null && (keywords.Any(k => tale.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new TaleDraftSummary()
                   {
                       Id = tale.Id,
                       Title = tale.Title.Value,
                       Status = tale.Status,
                       Category = tale.Category,
                       Summary = tale.Summary!.Value,
                       Details = tale.Details!.Value,
                       PhotoUrl = tale.PhotoUrl!.Value,
                       Date = tale.CreationDate,
                       Country = tale.Country,
                       TaleUrl = tale.Url!.Value,
                       Tags = (from tag in _dbContext.TaleTags
                               where tag.TaleId == tale.Id
                               select tag.Title.Value).ToList(),
                       History = (from history in _dbContext.TaleHistories
                                  where history.TaleId == tale.Id
                                  orderby history.Date ascending
                                  join account in _dbContext.Accounts
                                  on history.AdminId equals account.Id
                                  select new TaleHistorySummary()
                                  {
                                      AdminId = history.AdminId,
                                      Date = history.Date,
                                      Status = history.Status,
                                      AdminUsername = account.Username.Value,
                                      Reasons = history.Reasons!.Value
                                  }).ToList()

                   };
        }

        private IQueryable<TaleDraftSummary> LoadAllDraftTalesIQueryable(string? keyword)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');
            return from tale in _dbContext.Tales
                   join account in _dbContext.Accounts
                   on tale.CreatorId equals account.Id

                   where keywords != null && (keywords.Any(k => tale.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new TaleDraftSummary()
                   {
                       Id = tale.Id,
                       WriterId = tale.CreatorId,
                       WriterUsername = account.Username.Value,
                       Title = tale.Title.Value,
                       Status = tale.Status,
                       Category = tale.Category,
                       Country = tale.Country,
                       Summary = tale.Summary!.Value,
                       Details = tale.Details!.Value,
                       PhotoUrl = tale.PhotoUrl!.Value,
                       Date = tale.CreationDate,
                       Tags = (from tag in _dbContext.TaleTags
                               where tag.TaleId == tale.Id
                               select tag.Title.Value).ToList(),
                       History = (from history in _dbContext.TaleHistories
                                  where history.TaleId == tale.Id
                                  orderby history.Date ascending
                                  join account in _dbContext.Accounts
                                  on history.AdminId equals account.Id
                                  select new TaleHistorySummary()
                                  {
                                      AdminId = history.AdminId,
                                      Date = history.Date,
                                      Status = history.Status,
                                      AdminUsername = account.Username.Value,
                                      Reasons = history.Reasons!.Value
                                  }).ToList(),
                       TaleUrl = tale.Url!.Value
                   };
        }

        private IQueryable<TaleLink> LoadTaleLinksIQueryable(Guid userId, string? keyword)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from tale in _dbContext.Tales
                   where tale.CreatorId == userId

                   where keywords != null && (keywords.Any(k => tale.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => tale.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new TaleLink()
                   {
                       Id = tale.Id,
                       Title = tale.Title.Value,
                       Date = tale.CreationDate,
                   };
        }

        private static IQueryable<TaleDraftSummary> SortTales(SortTypes? sort, IQueryable<TaleDraftSummary> tales)
        {
            return sort switch
            {
                SortTypes.Most_Recent => tales.OrderByDescending(s => s.Date),
                SortTypes.Least_Recent => tales.OrderBy(s => s.Date),
                _ => tales.OrderByDescending(s => s.Date)
            };
        }

        private static IQueryable<TaleLink> SortTaleLinks(SortTypes? sort, IQueryable<TaleLink> tales)
        {
            return sort switch
            {
                SortTypes.Most_Recent => tales.OrderByDescending(s => s.Date),
                SortTypes.Least_Recent => tales.OrderBy(s => s.Date),
                _ => tales.OrderByDescending(s => s.Date)
            };
        }

        #endregion
    }

}
