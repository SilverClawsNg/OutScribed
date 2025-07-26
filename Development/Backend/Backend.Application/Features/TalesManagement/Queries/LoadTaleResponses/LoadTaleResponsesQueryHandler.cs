using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleResponses
{
    public class LoadTaleResponsesQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadTaleResponsesQuery, LoadTaleResponsesQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadTaleResponsesQueryResponse> Handle(LoadTaleResponsesQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.TaleRepository.LoadTaleResponses(request.AccountId, request.ParentId, request.Username, request.Keyword, request.Sort, request.Pointer ?? 0, request.Size ?? 5);

            response.Username = request.Username;
            response.Keyword = request.Keyword;

            response.ParentId = request.ParentId;
            response.Sort = request.Sort ?? Domain.Enums.SortTypes.Most_Recent;
            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 5;
            response.More = response.Responses != null && response.Responses.Count > (request.Size ?? 5);
            response.Responses = response.Responses?.Take(request.Size ?? 5).ToList();

            return response;
        }
    }

}
