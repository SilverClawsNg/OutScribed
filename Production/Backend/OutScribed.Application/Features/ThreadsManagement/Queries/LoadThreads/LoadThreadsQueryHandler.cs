using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreads
{
    public class LoadThreadsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadThreadsQuery, LoadThreadsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadThreadsQueryResponse> Handle(LoadThreadsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.ThreadsRepository.LoadThreads(request.AccountId, request.ThreaderId, request.Category, request.Country, request.Username, request.Sort, request.Tag, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Keyword = request.Keyword;
            response.Category = request.Category;
            response.Country = request.Country;
            response.Username = request.Username;
            response.Tag = request.Tag;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Threads is null ? 0 : response.Threads.Count) > (request.Size ?? 20);

            response.Threads = response.Threads?.Take(request.Size ?? 20).ToList();

            return response;
        }
    }

}
