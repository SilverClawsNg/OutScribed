using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Queries.LoadThreadDrafts
{
    public class LoadThreadDraftsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadThreadDraftsQuery, LoadThreadDraftsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadThreadDraftsQueryResponse> Handle(LoadThreadDraftsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.ThreadsRepository.LoadThreadDrafts(request.AccountId, request.Category, request.Country, request.IsOnline, request.Sort, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Keyword = request.Keyword;
            response.Category = request.Category;
            response.Country = request.Country;
            response.IsOnline = request.IsOnline;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Threads is null ? 0 : response.Threads.Count) > (request.Size ?? 20);

            response.Threads = response.Threads?.Take(request.Size ?? 20).ToList();

            return response;
        }
    }

}
