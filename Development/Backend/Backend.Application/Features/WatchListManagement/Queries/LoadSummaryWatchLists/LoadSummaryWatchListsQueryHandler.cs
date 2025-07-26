using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.WatchListManagement.Queries.LoadSummaryWatchLists
{
    public class LoadSummaryWatchListsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadSummaryWatchListsQuery, LoadSummaryWatchListsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadSummaryWatchListsQueryResponse> Handle(LoadSummaryWatchListsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.WatchListRepository.LoadSummaryWatchLists(request.Category, request.Country, request.Sort, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Keyword = request.Keyword;
            response.Category = request.Category;
            response.Country = request.Country;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.WatchLists is null ? 0 : response.WatchLists.Count) > (request.Size ?? 20);

            response.WatchLists = response.WatchLists?.Take(request.Size ?? 20).ToList();
           
            return response;
        }
    }

}
