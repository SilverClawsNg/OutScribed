using Backend.Application.Features.WatchListManagement.Common;
using Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchList;
using Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchLists;
using Backend.Application.Features.WatchListManagement.Queries.LoadSummaryWatchLists;
using Backend.Application.Repositories;
using Backend.Domain.Enums;
using Backend.Domain.Models.WatchListManagement.Entities;
using Backend.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.Repositories
{
    public class WatchListRepository(BackendDbContext dbContext)
        : IWatchListRepository
    {
        private readonly BackendDbContext _dbContext = dbContext;

        public async Task<WatchList?> GetWatchListById(Guid watchlistId)
        {
            return await (from watchlist in _dbContext.WatchLists
                          where watchlist.Id == watchlistId
                          select watchlist)
                          .Include(c => c.Tales)
                          .Include(c => c.Followers)
               .SingleOrDefaultAsync();
        }

        public async Task<WatchListSummary?> LoadWatchList(Guid watchListId)
        {

            return await (from watchList in _dbContext.WatchLists
                          where watchList.Id == watchListId

                          select new WatchListSummary()
                          {
                              Id = watchList.Id,
                              Title = watchList.Title.Value,
                              Category = watchList.Category,
                              Country = watchList.Country,
                              Summary = watchList.Summary!.Value,
                              SourceUrl = watchList.Source.Url,
                              SourceText = watchList.Source.Text,
                              Tales = watchList.Tales.Count,
                              Date = watchList.Date,
                              Followers = watchList.Followers.Count,

                          }).SingleOrDefaultAsync();
        }

        public async Task<LoadFullWatchListQueryResponse?> LoadFullWatchList(Guid watchListId, Guid accountId)
        {
          
            return await  (from watchList in _dbContext.WatchLists
                           where watchList.Id == watchListId
                           select new LoadFullWatchListQueryResponse()
                   {
                       Id = watchList.Id,
                       Title = watchList.Title.Value,
                       Category = watchList.Category,
                       Summary = watchList.Summary!.Value,
                       SourceUrl = watchList.Source.Url,
                       SourceText = watchList.Source.Text,
                       Tales = watchList.Tales.Count,
                       Date = watchList.Date,
                       Followers = watchList.Followers.Count,
                       Country = watchList.Country,
                       IsFollowingWatchlist = watchList.Followers.Any(c => c.FollowerId == accountId),

                           }).SingleOrDefaultAsync();
        }


        public async Task<LoadSummaryWatchListsQueryResponse> LoadSummaryWatchLists(Categories? category, Countries? country,
            SortTypes? sort, string? keyword, int pointer, int size)
        {

            //Get watchLists
            IQueryable<WatchListSummary> watchLists = LoadSummaryWatchListsIQueryable(keyword);

            if (category != null)
                watchLists = watchLists.Where(c => c.Category == category);

            if (country != null)
                watchLists = watchLists.Where(c => c.Country == country);

            //Sort functions
            watchLists = SortWatchLists(sort, watchLists);

            //Return functions
            return new LoadSummaryWatchListsQueryResponse()
            {
                WatchLists = await watchLists.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await watchLists.CountAsync()
            };
        }

        public async Task<LoadFullWatchListsQueryResponse> LoadFullWatchLists(Guid accountId, Categories? category,
         Countries? country, SortTypes? sort, string? keyword, int pointer, int size)
        {

            //Get watchLists
            IQueryable<WatchListSummary> watchLists = LoadFullWatchListsIQueryable(keyword, accountId);

            if (category != null)
                watchLists = watchLists.Where(c => c.Category == category);

            if (country != null)
                watchLists = watchLists.Where(c => c.Country == country);

            //Sort functions
            watchLists = SortWatchLists(sort, watchLists);

            //Return functions
            return new LoadFullWatchListsQueryResponse()
            {
                WatchLists = await watchLists.Skip(pointer * size).Take(size + 1).ToListAsync(),
                Counter = await watchLists.CountAsync()
            };
        }

        public async Task<List<WatchListSummary>> LoadMostRecentWatchlists(Guid accountId)
        {
          
            return await(from watchList in _dbContext.WatchLists

                   select new WatchListSummary()
                   {
                       Id = watchList.Id,
                       Title = watchList.Title.Value,
                       Category = watchList.Category,
                        Country = watchList.Country,
                       Summary = watchList.Summary!.Value,
                       SourceUrl = watchList.Source.Url,
                       SourceText = watchList.Source.Text,
                       Tales = watchList.Tales.Count,
                       Date = watchList.Date,
                       Followers = watchList.Followers.Count,
                       IsFollowingWatchlist = watchList.Followers.Any(c => c.FollowerId == accountId),

                   }).OrderByDescending(c => c.Date).Take(12).ToListAsync();
        }


        #region Helpers

        private IQueryable<WatchListSummary> LoadSummaryWatchListsIQueryable(string? keyword)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');
            return from watchList in _dbContext.WatchLists

                   where keywords != null && (keywords.Any(k => watchList.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => watchList.Summary!.Value.ToLower().Contains(k.ToLower())))

                   select new WatchListSummary()
                   {
                       Id = watchList.Id,
                       Title = watchList.Title.Value,
                       Category = watchList.Category,
                       Country = watchList.Country,
                       Summary = watchList.Summary!.Value,
                       SourceUrl = watchList.Source.Url,
                       SourceText = watchList.Source.Text,
                       Tales = watchList.Tales.Count,
                       Date = watchList.Date,
                       Followers = watchList.Followers.Count,
                   };
        }

        private static IQueryable<WatchListSummary> SortWatchLists(SortTypes? sort, IQueryable<WatchListSummary> watchLists)
        {
            return sort switch
            {
                SortTypes.Most_Recent => watchLists.OrderByDescending(s => s.Date),
                SortTypes.Least_Recent => watchLists.OrderBy(s => s.Date),
                SortTypes.Most_Followed => watchLists.OrderByDescending(s => s.Followers),
                _ => watchLists.OrderByDescending(s => s.Date)
            };
        }

        private IQueryable<WatchListSummary> LoadFullWatchListsIQueryable(string? keyword, Guid accountId)
        {
            //check if keyword is null
            keyword ??= string.Empty;

            //split keyword
            var keywords = keyword.Split(' ');
            return from watchList in _dbContext.WatchLists

                   where keywords != null && (keywords.Any(k => watchList.Title.Value.ToLower().Contains(k.ToLower()))
                   || keywords.Any(k => watchList.Summary!.Value.ToLower().Contains(k.ToLower())))

                   select new WatchListSummary()
                   {
                       Id = watchList.Id,
                       Title = watchList.Title.Value,
                       Category = watchList.Category,
                       Summary = watchList.Summary!.Value,
                       SourceUrl = watchList.Source.Url,
                       SourceText = watchList.Source.Text,
                       Tales = watchList.Tales.Count,
                       Date = watchList.Date,
                       Followers = watchList.Followers.Count,
                       Country = watchList.Country,
                       IsFollowingWatchlist = watchList.Followers.Any(c => c.FollowerId == accountId),
                     
                   };
        }

        #endregion

    }
}
