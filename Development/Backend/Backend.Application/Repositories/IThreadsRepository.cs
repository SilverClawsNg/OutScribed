using Backend.Application.Features.ThreadsManagement.Common;
using Backend.Application.Features.ThreadsManagement.Queries.LoadTaleThreads;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThread;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreadComments;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreadResponses;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreads;
using Backend.Application.Features.ThreadsManagement.Queries.LoadThreadDrafts;
using Backend.Domain.Enums;
using Backend.Domain.Models.ThreadsManagement;

namespace Backend.Application.Repositories
{
    public interface IThreadsRepository
    {

        /// <summary>
        /// Loads a single thread
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        Task<Threads?> GetThreadById(Guid threadId);

        /// <summary>
        /// Loads threads
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadTaleThreadsQueryResponse> LoadTaleThreads(Guid accountId, Guid id, Categories? category, SortTypes? sort, string? keyword, int pointer, int size);

        /// <summary>
        /// Load a thread
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<LoadThreadQueryResponse?> LoadThread(string url, Guid accountId);

        /// <summary>
        /// Loads a list of threads
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="threaderId"></param>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadThreadsQueryResponse> LoadThreads(Guid accountId, Guid? threaderId, Categories? category,
           Countries? country, string? username, SortTypes? sort, string? tag, string? keyword, int pointer, int size);

        /// <summary>
        /// Loads threads comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<ThreadCommentSummary?> LoadThreadComment(Guid accountId, Guid commentId);

        /// <summary>
        /// Load a thread's comments
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="threadId"></param>
        /// <param name="sort"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadThreadCommentsQueryResponse> LoadThreadComments(Guid accountId, Guid threadId,
         string? username, string? keyword, SortTypes? sort, int pointer, int size);

        /// <summary>
        /// Loads thread's responses
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="commentId"></param>
        /// <param name="sort"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadThreadResponsesQueryResponse> LoadThreadResponses(Guid accountId, Guid parentId,
            string? username, string? keyword, SortTypes? sort, int pointer, int size);
        
        /// <summary>
        /// Load a user's threads
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="category"></param>
        /// <param name="sort"></param>
        /// <param name="keyword"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadThreadDraftsQueryResponse> LoadThreadDrafts(Guid accountId, Categories? category, 
            Countries? country, bool? isOnline, SortTypes? sort, string? keyword, int pointer, int size);

        /// <summary>
        /// Loads a thread addendum
        /// </summary>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        Task<ThreadAddendumSummary?> LoadThreadAddendum(Guid addendumId);

        /// <summary>
        /// Loads most popular threads
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<ThreadsSummary>> LoadMostPopularThreads(Guid accountId);

        /// <summary>
        /// Load most recent threads
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<ThreadsSummary>> LoadMostRecentThreads(Guid accountId);

        /// <summary>
        /// Gets a thread's tags
        /// </summary>
        /// <param name="draftId"></param>
        /// <returns></returns>
        Task<List<string>?> GetThreadTags(Guid threadId);

        /// <summary>
        /// Load thread header
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        Task<ThreadHeaderSummary?> LoadThreadHeaderSummary(Guid accountId, Guid threadId);
    }
}
