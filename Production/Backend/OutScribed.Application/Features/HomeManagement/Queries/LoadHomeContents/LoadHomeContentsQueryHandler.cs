using OutScribed.Application.Features.WatchListManagement.Common;
using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.HomeManagement.Queries.LoadHomeContents
{
    public class LoadHomeContentsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadHomeContentsQuery, LoadHomeContentsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadHomeContentsQueryResponse> Handle(LoadHomeContentsQuery request, CancellationToken cancellationToken)
        {

            return new LoadHomeContentsQueryResponse()
            {
                PopularTales = await _unitOfWork.TaleRepository.LoadMostPopularTales(request.AccountId),
                RecentTales = await _unitOfWork.TaleRepository.LoadMostRecentTales(request.AccountId),
                RecentWriters = await _unitOfWork.UserRepository.LoadMostRecentWriters(request.AccountId),
                PopularWriters = await _unitOfWork.UserRepository.LoadMostPopularWriters(request.AccountId),
                PopularThreads = await _unitOfWork.ThreadsRepository.LoadMostPopularThreads(request.AccountId),
                RecentThreads = await _unitOfWork.ThreadsRepository.LoadMostRecentThreads(request.AccountId),
                RecentWatchLists = AddCount(await _unitOfWork.WatchListRepository.LoadMostRecentWatchlists(request.AccountId))
            };
        }

        private static List<WatchListSummary>? AddCount(List<WatchListSummary> watchlists)
        {
            if (watchlists == null)
                return watchlists;

            for (var i = 0; i < watchlists.Count; i++)
            {
                watchlists[i].Count = i + 1;
            }

            return [.. watchlists];
        }
    }

}
