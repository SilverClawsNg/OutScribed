using Backend.Application.Features.ThreadsManagement.Common;
using Backend.Application.Repositories;
using Backend.Domain.Enums;
using Backend.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreads;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThread;
using Backend.Application.Features.ThreadsManagement.Queries.LoadTaleThreads;
using Backend.Domain.Models.ThreadsManagement;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreadComments;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreadResponses;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreadDrafts;

namespace Backend.Persistence.Repositories
{
    public class ThreadsRepository(BackendDbContext dbContext)
        : IThreadsRepository
    {

        private readonly BackendDbContext _dbContext = dbContext;

        public async Task<Threads?> GetThreadById(Guid threadId)
        {
            return await (from threads in _dbContext.Threads
                          where threads.Id == threadId
                          select threads)
                          .Include(c => c.Ratings)
                          .Include(c => c.Comments)
                          .Include(c => c.Flags)
                          .Include(c => c.Tags)
                          .Include(c => c.Followers)
               .SingleOrDefaultAsync();
        }

        public async Task<List<string>?> GetThreadTags(Guid draftId)
        {
            return await (from tag in _dbContext.ThreadTags
                          where tag.ThreadsId == draftId
                          select tag.Title.Value)
               .ToListAsync();
        }

        public async Task<ThreadAddendumSummary?> LoadThreadAddendum(Guid addendumId)
        {

            return await (from addendum in _dbContext.ThreadsAddendums
                          where addendum.Id == addendumId
                          select new ThreadAddendumSummary()
                          {
                              Date = addendum.Date,
                              Details = addendum.Details.Value
                          }).SingleOrDefaultAsync();
        }

        public async Task<LoadThreadQueryResponse?> LoadThread(string url, Guid accountId)
        {

            return await (from thread in _dbContext.Threads
                          where thread.Url.Value == url
                          join tale in _dbContext.Tales
                          on thread.TaleId equals tale.Id
                         
                          join account in _dbContext.Accounts
                          on thread.ThreaderId equals account.Id

                          select new LoadThreadQueryResponse()
                          {
                              Id = thread.Id,
                              Title = thread.Title.Value,
                              ThreaderUsername = account.Username.Value,
                              ThreaderId = thread.ThreaderId,
                              PhotoUrl = thread.PhotoUrl!.Value,
                              Summary = thread.Summary!.Value,
                              Category = thread.Category,
                              Country = thread.Country,
                              TaleTitle = tale.Title.Value,
                              TaleUrl = tale.Url!.Value,
                              Details = thread.Details!.Value,
                              Date = thread.Date,
                              Views = thread.Views,
                              Likes = thread.Ratings.Count(c => c.Type == RateTypes.Like),
                              Hates = thread.Ratings.Count(c => c.Type == RateTypes.Hate),
                              Shares = thread.Sharers.Count,
                              CommentsCount = thread.Comments.Count,
                              Followers = thread.Followers.Count,
                              ThreaderProfileViews = account.Views,
                              ThreaderFollowers = account.Followers.Count,
                              HasFlagged = tale.Flags.Any(c => c.FlaggerId == accountId),
                              Flags = tale.Flags.Count,
                              IsFollowingThreader = account.Followers.Any(c => c.FollowerId == accountId),
                              IsFollowingThread = thread.Followers
                                                       .Any(c => c.FollowerId == accountId),
                              MyRatings = thread.Ratings
                                                      .Where(c => c.RaterId == accountId)
                                                      .Select(c => c.Type)
                                                      .SingleOrDefault(),
                              Addendums = (from addendum in _dbContext.ThreadsAddendums
                                           where addendum.ThreadsId == thread.Id
                                           select new ThreadAddendumSummary()
                                           {
                                               Date = addendum.Date,
                                               Details = addendum.Details.Value
                                           }).ToList(),
                              Tags = (from tag in _dbContext.ThreadTags
                                      where tag.ThreadsId == thread.Id
                                      select tag.Title.Value).ToList(),


                          }).SingleOrDefaultAsync();
        }

        public async Task<ThreadHeaderSummary?> LoadThreadHeaderSummary(Guid accountId, Guid threadId)
        {

            return await (from thread in _dbContext.Threads
                          where thread.Id == threadId
                          join account in _dbContext.Accounts
                          on thread.ThreaderId equals account.Id
                          select new ThreadHeaderSummary()
                          {
                              Id = thread.Id,
                              Title = thread.Title.Value,
                              ThreadUrl = thread.Url!.Value,
                              Category = thread.Category,
                              Country = thread.Country,
                              Summary = thread.Summary!.Value,
                              Date = thread.Date,
                              ThreaderId = account.Id,
                              ThreaderUsername = account.Username.Value,
                              IsFollowingThread = thread.Followers.Any(c => c.FollowerId == accountId)
                          }).SingleOrDefaultAsync();
        }

        public async Task<LoadThreadDraftsQueryResponse> LoadThreadDrafts(Guid accountId, Categories? category,
           Countries? country, bool? isOnline, SortTypes? sort, string? keyword, int pointer, int size)
        {

            //Get threads
            IQueryable<ThreadDraftSummary> threads = LoadThreadDraftsIQueryable(keyword, accountId);

            if (category != null)
                threads = threads.Where(c => c.Category == category);

            if (country != null)
                threads = threads.Where(c => c.Country == country);

            if (isOnline != null)
                threads = threads.Where(c => c.IsOnline == isOnline);

            //Sort functions
            threads = SortUserThreads(sort, threads);

            //Return functions
            return new LoadThreadDraftsQueryResponse()
            {
                Threads = await threads.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await threads.CountAsync()
            };
        }

        public async Task<LoadThreadsQueryResponse> LoadThreads(Guid accountId, Guid? threaderId, Categories? category,
           Countries? country, string? username, SortTypes? sort, string? tag, string? keyword, int pointer, int size)
        {

            //Get threads
            IQueryable<ThreadsSummary> threads = tag != null ? LoadThreadsByTagsIQueryable(tag, accountId) :
                LoadThreadsIQueryable(keyword, accountId);

            if (category != null)
                threads = threads.Where(c => c.Category == category);

            if (country != null)
                threads = threads.Where(c => c.Country == country);

            if (threaderId != null)
                threads = threads.Where(c => c.ThreaderId == threaderId);

            if (username != null)
                threads = threads.Where(c => c.ThreaderUsername.ToLower() == username.Trim('@').ToLower());

            //Sort functions
            threads = SortThreads(sort, threads);

            //Return functions
            return new LoadThreadsQueryResponse()
            {
                Threads = await threads.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await threads.CountAsync()
            };
        }

        public async Task<LoadTaleThreadsQueryResponse> LoadTaleThreads(Guid accountId, Guid Id, Categories? category,
        SortTypes? sort, string? keyword, int pointer, int size)
        {

            //Get threads
            IQueryable<ThreadsSummary> threads = LoadTaleThreadsIQueryable(Id, keyword, accountId);

            if (category != null)
                threads = threads.Where(c => c.Category == category);

            //Sort functions
            threads = SortThreads(sort, threads);

            //Return functions
            return new LoadTaleThreadsQueryResponse()
            {
                Threads = await threads.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await threads.CountAsync()
            };
        }

        public async Task<ThreadCommentSummary?> LoadThreadComment(Guid accountId, Guid commentId)
        {

            return await (from comment in _dbContext.ThreadsComments
                          where comment.Id == commentId
                          join account in _dbContext.Accounts
                          on comment.CommentatorId equals account.Id
                          join profile in _dbContext.Profiles
                           on account.Id equals profile.AccountId
                          select new ThreadCommentSummary()
                          {
                              Id = comment.Id,
                              Details = comment.Details.Value,
                              ThreadId = comment.ThreadsId,
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

        public async Task<LoadThreadCommentsQueryResponse> LoadThreadComments(Guid accountId, Guid threadId,
         string? username, string? keyword, SortTypes? sort, int pointer, int size)
        {

            //Get posts
            IQueryable<ThreadCommentSummary> comments = LoadCommentSummaryIQueryable(threadId, accountId, keyword);

            if (username != null)
                comments = comments.Where(c => c.CommentatorUsername.ToLower() == username.Trim('@').ToLower());

            //Sort functions
            comments = SortComments(sort, comments);

            //Return functions
            return new LoadThreadCommentsQueryResponse()
            {
                Comments = await comments.Skip(pointer).Take(size + 1).ToListAsync(),
                Counter = await comments.CountAsync()
            };
        }

        public async Task<LoadThreadResponsesQueryResponse> LoadThreadResponses(Guid accountId, Guid parentId,
        string? username, string? keyword, SortTypes? sort, int pointer, int size)
        {

            //Get posts
            IQueryable<ThreadCommentSummary> responses = LoadResponseSummaryIQueryable(parentId, accountId, keyword);

            if (username != null)
                responses = responses.Where(c => c.CommentatorUsername.ToLower() == username.Trim('@').ToLower());

            //Sort functions
            responses = SortComments(sort, responses);

            //Return functions
            return new LoadThreadResponsesQueryResponse()
            {
                Responses = await responses.Skip(pointer).Take(size + 1).ToListAsync(),
                Counter = await responses.CountAsync()
            };
        }

        public async Task<List<ThreadsSummary>> LoadMostRecentThreads(Guid accountId)
        {
         
            return await (from thread in _dbContext.Threads
                   where thread.IsOnline == true
                          join tale in _dbContext.Tales
                                on thread.TaleId equals tale.Id
                          join account in _dbContext.Accounts
                   on thread.ThreaderId equals account.Id

                   orderby thread.Date

                   select new ThreadsSummary()
                   {
                       Id = thread.Id,
                       ThreaderId = thread.ThreaderId,
                       ThreadUrl = thread.Url.Value,
                       ThreaderUsername = account.Username.Value,
                       Title = thread.Title.Value,
                     
                       PhotoUrl = thread.PhotoUrl!.Value,
                       Summary = thread.Summary!.Value,
                       Category = thread.Category,
                       Country = thread.Country,
                       Date = thread.Date,
                       Views = thread.Views,
                       Likes = thread.Ratings.Count(c => c.Type == RateTypes.Like),
                       Hates = thread.Ratings.Count(c => c.Type == RateTypes.Hate),
                       Comments = thread.Comments.Count,
                       Followers = thread.Followers.Count,
                       IsFollowingThread = thread.Followers.Any(c => c.FollowerId == accountId),

                   }).OrderByDescending(c => c.Date).Take(3).ToListAsync();
        }

        public async Task<List<ThreadsSummary>> LoadMostPopularThreads(Guid accountId)
        {

            return await (from thread in _dbContext.Threads
                          where thread.IsOnline == true
                          join tale in _dbContext.Tales
                         on thread.TaleId equals tale.Id
                          join account in _dbContext.Accounts
                          on thread.ThreaderId equals account.Id

                          orderby thread.Ratings.Count(c => c.Type == RateTypes.Like) 

                          select new ThreadsSummary()
                          {
                              Id = thread.Id,
                              ThreaderId = thread.ThreaderId,
                              ThreadUrl = thread.Url.Value,
                              ThreaderUsername = account.Username.Value,
                              Title = thread.Title.Value,
                              PhotoUrl = thread.PhotoUrl!.Value,
                              Summary = thread.Summary!.Value,
                              Category = thread.Category,
                              Country = thread.Country,
                            
                              Date = thread.Date,
                              Views = thread.Views,
                              Likes = thread.Ratings.Count(c => c.Type == RateTypes.Like),
                              Hates = thread.Ratings.Count(c => c.Type == RateTypes.Hate),
                              Comments = thread.Comments.Count,
                              Followers = thread.Followers.Count,
                              IsFollowingThread = thread.Followers.Any(c => c.FollowerId == accountId),

                          }).OrderByDescending(c => c.Views).Take(4).ToListAsync();
        }

        #region Helpers

        private IQueryable<ThreadsSummary> LoadTaleThreadsIQueryable(Guid taleId, string? keyword, Guid accountId)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');
            return from thread in _dbContext.Threads
                   where thread.TaleId == taleId && thread.IsOnline == true

                   join account in _dbContext.Accounts
                   on thread.ThreaderId equals account.Id

                   where keywords != null && (keywords.Any(k => thread.Title.Value.ToLower().Contains(k.ToLower()))
                 || keywords.Any(k => thread.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => thread.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new ThreadsSummary()
                   {
                       Id = thread.Id,
                       ThreaderId = thread.ThreaderId,
                       ThreaderUsername = account.Username.Value,
                       Title = thread.Title.Value,
                       ThreadUrl = thread.Url.Value,
                       Category = thread.Category,
                       Country = thread.Country,
                       Summary = thread.Summary!.Value,
                       IsFollowingThread = thread.Followers.Any(c => c.FollowerId == accountId),
                       Date = thread.Date,
                       Views = thread.Views,
                       Likes = thread.Ratings.Count(c => c.Type == RateTypes.Like),
                       Hates = thread.Ratings.Count(c => c.Type == RateTypes.Hate),
                       Comments = thread.Comments.Count,
                       Followers = thread.Followers.Count,
                   };
        }

        private IQueryable<ThreadsSummary> LoadThreadsIQueryable(string? keyword, Guid accountId)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');
            return from thread in _dbContext.Threads
                   where thread.IsOnline == true
                   join tale in _dbContext.Tales
                         on thread.TaleId equals tale.Id
                   join account in _dbContext.Accounts
                   on thread.ThreaderId equals account.Id

                   where keywords != null && (keywords.Any(k => thread.Title.Value.ToLower().Contains(k.ToLower()))
                  || keywords.Any(k => thread.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => thread.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new ThreadsSummary()
                   {
                       Id = thread.Id,
                       ThreaderId = thread.ThreaderId,
                       ThreadUrl = thread.Url.Value,
                       ThreaderUsername = account.Username.Value,
                       Title = thread.Title.Value,
                       PhotoUrl = thread.PhotoUrl!.Value,
                       Summary = thread.Summary!.Value,
                       Category = thread.Category,
                       Country = thread.Country,
                       Date = thread.Date,
                       Views = thread.Views,
                       Likes = thread.Ratings.Count(c=>c.Type == RateTypes.Like),
                       Hates = thread.Ratings.Count(c => c.Type == RateTypes.Hate),
                       IsFollowingThread = thread.Followers.Any(c => c.FollowerId == accountId),
                       Comments = thread.Comments.Count,
                       Followers = thread.Followers.Count,
                      
                       Tags = (from tag in _dbContext.ThreadTags
                               where tag.ThreadsId == thread.Id
                               select tag.Title.Value).ToList(),
                   };
        }

        private IQueryable<ThreadsSummary> LoadThreadsByTagsIQueryable(string tag, Guid accountId)
        {
            //check if keyword is null
           
            return from threadTag in _dbContext.ThreadTags
                   where threadTag.Title.Value.ToLower() == tag.ToLower()
                   join thread in _dbContext.Threads
                   on threadTag.ThreadsId equals thread.Id
                   where thread.IsOnline == true
                   join tale in _dbContext.Tales
                         on thread.TaleId equals tale.Id
                   join account in _dbContext.Accounts
                   on thread.ThreaderId equals account.Id

                   select new ThreadsSummary()
                   {
                       Id = thread.Id,
                       ThreaderId = thread.ThreaderId,
                       ThreadUrl = thread.Url.Value,
                       ThreaderUsername = account.Username.Value,
                       Title = thread.Title.Value,
                       PhotoUrl = thread.PhotoUrl!.Value,
                       Summary = thread.Summary!.Value,
                       Category = thread.Category,
                       Country = thread.Country,
                       Date = thread.Date,
                       Views = thread.Views,
                       Likes = thread.Ratings.Count(c => c.Type == RateTypes.Like),
                       Hates = thread.Ratings.Count(c => c.Type == RateTypes.Hate),
                       IsFollowingThread = thread.Followers.Any(c => c.FollowerId == accountId),
                       Comments = thread.Comments.Count,
                       Followers = thread.Followers.Count,

                       Tags = (from tag in _dbContext.ThreadTags
                               where tag.ThreadsId == thread.Id
                               select tag.Title.Value).ToList(),
                   };
        }

        private IQueryable<ThreadDraftSummary> LoadThreadDraftsIQueryable(string? keyword, Guid accountId)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');
            return from thread in _dbContext.Threads
                   where thread.ThreaderId == accountId
                   join tale in _dbContext.Tales
                         on thread.TaleId equals tale.Id
                   join account in _dbContext.Accounts
                   on thread.ThreaderId equals account.Id

                   where keywords != null && (keywords.Any(k => thread.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => thread.Summary!.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => thread.Details!.Value.ToLower().Contains(k.ToLower())))

                   select new ThreadDraftSummary()
                   {
                       Id = thread.Id,
                       ThreadUrl = thread.Url.Value,
                       Title = thread.Title.Value,
                       Summary = thread.Summary!.Value,
                       PhotoUrl = thread.PhotoUrl!.Value,
                       Details = thread.Details!.Value,
                       Category = thread.Category,
                       Country = thread.Country,
                       TaleTitle = tale.Title.Value,
                       TaleUrl = tale.Url!.Value,
                       Date = thread.Date,
                       IsOnline = thread.IsOnline,
                       Addendums = (from addendum in _dbContext.ThreadsAddendums
                                    where addendum.ThreadsId == thread.Id
                                    select new ThreadAddendumSummary()
                                    {
                                        Date = addendum.Date,
                                        Details = addendum.Details.Value
                                    }).ToList(),
                       Tags = (from tag in _dbContext.ThreadTags
                               where tag.ThreadsId == thread.Id
                               select tag.Title.Value).ToList(),
                   };
        }

        private static IQueryable<ThreadsSummary> SortThreads(SortTypes? sort, IQueryable<ThreadsSummary> threads)
        {
            return sort switch
            {
                SortTypes.Most_Recent => threads.OrderByDescending(s => s.Date),
                SortTypes.Least_Recent => threads.OrderBy(s => s.Date),
                _ => threads.OrderByDescending(s => s.Date)
            };
        }

        private static IQueryable<ThreadDraftSummary> SortUserThreads(SortTypes? sort, IQueryable<ThreadDraftSummary> threads)
        {
            return sort switch
            {
                SortTypes.Most_Recent => threads.OrderByDescending(s => s.Date),
                SortTypes.Least_Recent => threads.OrderBy(s => s.Date),
                _ => threads.OrderByDescending(s => s.Date)
            };
        }

        private IQueryable<ThreadCommentSummary> LoadCommentSummaryIQueryable(Guid threadId, Guid accountId, string? keyword)
        {

            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from comment in _dbContext.ThreadsComments
                   where comment.ThreadsId == threadId && comment.ParentId == null
                   join account in _dbContext.Accounts
                   on comment.CommentatorId equals account.Id
                   join profile in _dbContext.Profiles
                    on account.Id equals profile.AccountId

                   where keywords != null && keywords.Any(k => comment.Details.Value.ToLower().Contains(k.ToLower()))
                   select new ThreadCommentSummary()
                   {
                       Id = comment.Id,
                       Details = comment.Details.Value,
                       ThreadId = comment.ThreadsId,
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

        private IQueryable<ThreadCommentSummary> LoadResponseSummaryIQueryable(Guid parentId, Guid accountId, string? keyword)
        {

            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');

            return from comment in _dbContext.ThreadsComments
                   where comment.ParentId == parentId
                   join account in _dbContext.Accounts
                   on comment.CommentatorId equals account.Id
                   join profile in _dbContext.Profiles
                    on account.Id equals profile.AccountId

                   where keywords != null && keywords.Any(k => comment.Details.Value.ToLower().Contains(k.ToLower()))
                   select new ThreadCommentSummary()
                   {
                       Id = comment.Id,
                       Details = comment.Details.Value,
                       ThreadId = comment.ThreadsId,
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

        private static IQueryable<ThreadCommentSummary> SortComments(SortTypes? sort, IQueryable<ThreadCommentSummary> comments)
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

        #endregion

    }
}
