using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchLists
{
    public class LoadFullWatchListsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadFullWatchListsQuery, LoadFullWatchListsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadFullWatchListsQueryResponse> Handle(LoadFullWatchListsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.WatchListRepository.LoadFullWatchLists(request.UserId, request.Category, request.Country, request.Sort, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

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
