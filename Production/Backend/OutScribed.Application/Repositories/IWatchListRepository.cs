using OutScribed.Application.Features.WatchListManagement.Common;
using OutScribed.Application.Features.WatchListManagement.Queries.LoadFullWatchList;
using OutScribed.Application.Features.WatchListManagement.Queries.LoadFullWatchLists;
using OutScribed.Application.Features.WatchListManagement.Queries.LoadSummaryWatchLists;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.WatchListManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Application.Repositories
{
    public interface IWatchListRepository
    {
        /// <summary>
        /// Loads a single watchlist
        /// </summary>
        /// <param name="watchListId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<WatchListSummary?> LoadWatchList(Guid watchListId);

        /// <summary>
        /// Loads a list of watchlist
        /// </summary>
        /// <param name="country"></param>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadSummaryWatchListsQueryResponse> LoadSummaryWatchLists(Categories? category,
           Countries? country, SortTypes? sort, string? keyword, int pointer, int size);

        /// <summary>
        /// Loads a list of watchlist
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadFullWatchListsQueryResponse> LoadFullWatchLists(Guid accountId, Categories? category,
           Countries? country, SortTypes? sort, string? keyword, int pointer, int size);

        /// <summary>
        /// Gets a single watchlist by its Id
        /// </summary>
        /// <param name="watchlistId"></param>
        /// <returns></returns>
        Task<WatchList?> GetWatchListById(Guid watchlistId);

        /// <summary>
        /// Load a full watchlist
        /// </summary>
        /// <param name="watchListId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<LoadFullWatchListQueryResponse?> LoadFullWatchList(Guid watchListId, Guid accountId);

        /// <summary>
        /// Loads most recent watchlists
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<WatchListSummary>> LoadMostRecentWatchlists(Guid accountId);

    }

}
