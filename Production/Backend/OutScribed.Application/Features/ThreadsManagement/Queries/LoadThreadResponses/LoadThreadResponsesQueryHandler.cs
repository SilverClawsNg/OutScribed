using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadResponses
{
    public class LoadThreadResponsesQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadThreadResponsesQuery, LoadThreadResponsesQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadThreadResponsesQueryResponse> Handle(LoadThreadResponsesQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.ThreadsRepository.LoadThreadResponses(request.AccountId, request.ParentId, request.Username, request.Keyword, request.Sort,  request.Pointer ?? 0, request.Size ?? 5);

            response.Username = request.Username;
            response.Keyword = request.Keyword;

            response.ParentId = request.ParentId;
            response.Sort = request.Sort ?? Domain.Enums.SortTypes.Most_Recent;
            response.Pointer = response.Responses == null || response.Responses.Count < (request.Size ?? 5) ? -1 : (request.Pointer ?? 0) + (request.Size ?? 5);
            response.Size = request.Size ?? 5;
            response.More = response.Responses != null && response.Responses.Count > (request.Size ?? 5);
            response.Responses = response.Responses?.Take(request.Size ?? 5).ToList();

            return response;
        }
    }

}
