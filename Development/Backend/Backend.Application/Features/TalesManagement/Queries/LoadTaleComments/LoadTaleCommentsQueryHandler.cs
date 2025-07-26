using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleComments
{
    public class LoadTaleCommentsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadTaleCommentsQuery, LoadTaleCommentsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadTaleCommentsQueryResponse> Handle(LoadTaleCommentsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.TaleRepository.LoadTaleComments(request.AccountId, request.TaleId, request.Username, request.Keyword, request.Sort, request.Pointer ?? 0, request.Size ?? 5);

            response.Username = request.Username;
            response.Keyword = request.Keyword;

            response.TaleId = request.TaleId;
            response.Sort = request.Sort ?? Domain.Enums.SortTypes.Most_Recent;
            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 5;
            response.More = response.Comments != null && response.Comments.Count > (request.Size ?? 5);
            response.Comments = response.Comments?.Take(request.Size ?? 5).ToList();

            return response;
        }
    }

}
