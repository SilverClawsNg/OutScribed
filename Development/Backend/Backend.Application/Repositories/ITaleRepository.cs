using Backend.Application.Features.TalesManagement.Common;
using Backend.Application.Features.TalesManagement.Queries.LoadAllTaleDrafts;
using Backend.Application.Features.TalesManagement.Queries.LoadAllTales;
using Backend.Application.Features.TalesManagement.Queries.LoadSelfTaleDrafts;
using Backend.Application.Features.TalesManagement.Queries.LoadTale;
using Backend.Application.Features.TalesManagement.Queries.LoadTaleComments;
using Backend.Application.Features.TalesManagement.Queries.LoadTaleLinks;
using Backend.Application.Features.TalesManagement.Queries.LoadTaleResponses;
using Backend.Domain.Enums;
using Backend.Domain.Models.TalesManagement.Entities;

namespace Backend.Application.Repositories
{
    public interface ITaleRepository
    {
        /// <summary>
        /// Loads all tales
        /// </summary>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadAllTalesQueryResponse> LoadAllTales(Guid accountId, Categories? category,
           Countries? country, string? username, SortTypes? sort, Guid? watchlistId, string? tag, string? keyword, int pointer, int size);

        /// <summary>
        /// Load a tale
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<LoadTaleQueryResponse?> LoadTale(string url, Guid accountId);

        /// <summary>
        /// Gets a tale by its Id
        /// </summary>
        /// <param name="taleId"></param>
        /// <returns></returns>
        Task<Tale?> GetTaleById(Guid taleId);

        /// <summary>
        /// Load a tale comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TaleCommentSummary?> LoadTaleComment(Guid accountId, Guid commentId);

        /// <summary>
        /// Load tale comments
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="taleId"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadTaleCommentsQueryResponse> LoadTaleComments(Guid accountId, Guid taleId,
          string? username, string? keyword, SortTypes? sort, int pointer, int size);

        /// <summary>
        /// Loads a comment's responses
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="commentId"></param>
        /// <param name="sort"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadTaleResponsesQueryResponse> LoadTaleResponses(Guid accountId, Guid parentId,
        string? username, string? keyword, SortTypes? sort, int pointer, int size);

        /// <summary>
        /// Load most popular tales
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<TaleSummary>> LoadMostPopularTales(Guid accountId);

        /// <summary>
        /// Load most recent tales
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<TaleSummary>> LoadMostRecentTales(Guid accountId);

        /// <summary>
        /// Get tale url and title
        /// </summary>
        /// <param name="taleId"></param>
        /// <returns></returns>
        Task<TaleBrief?> GetTaleBrief(Guid taleId);

        /// <summary>
        /// Load newly created tale
        /// </summary>
        /// <param name="taleId"></param>
        /// <returns></returns>
        Task<TaleDraftSummary?> LoadCreateTaleResponse(Guid taleId);

        /// <summary>
        /// Gets a tale's tags
        /// </summary>
        /// <param name="taleId"></param>
        /// <returns></returns>
        Task<List<string>?> GetTaleTags(Guid taleId);

        /// <summary>
        /// Load a user's tale drafts
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="category"></param>
        /// <param name="country"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadSelfTaleDraftsQueryResponse> LoadSelfTaleDrafts(Guid userId,
          TaleStatuses? status, Categories? category, Countries? country, SortTypes? sort,
          string? keyword, int pointer, int size);

        /// <summary>
        /// Loads all tale drafts
        /// </summary>
        /// <param name="status"></param>
        /// <param name="category"></param>
        /// <param name="country"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadAllTaleDraftsQueryResponse> LoadAllTaleDrafts(TaleStatuses? status,
            Categories? category, Countries? country, string? username, SortTypes? sort, string? keyword, int pointer,
           int size);

        /// <summary>
        /// Loads details of a tale for comments page
        /// </summary>
        /// <param name="taleId"></param>
        /// <returns></returns>
        Task<TaleHeaderSummary?> LoadTaleHeaderSummary(Guid accountId, Guid taleId);

        /// <summary>
        /// Load tale links
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadTaleLinksQueryResponse> LoadTaleLinks(Guid userId,
         SortTypes? sort, string? keyword, int pointer, int size);
    }
}
