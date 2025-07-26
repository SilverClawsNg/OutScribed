using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadTaleThreads
{
    public class LoadTaleThreadsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadTaleThreadsQuery, LoadTaleThreadsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadTaleThreadsQueryResponse> Handle(LoadTaleThreadsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.ThreadsRepository.LoadTaleThreads(request.AccountId, request.Id, request.Category, request.Sort, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.TaleId = request.Id;

            response.Keyword = request.Keyword;
            response.Category = request.Category;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Threads is null ? 0 : response.Threads.Count) > (request.Size ?? 20);

            response.Threads = response.Threads?.Take(request.Size ?? 20).ToList();

            return response;
        }
    }

}
