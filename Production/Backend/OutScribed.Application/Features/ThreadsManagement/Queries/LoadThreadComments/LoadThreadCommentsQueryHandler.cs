using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadComments
{
    public class LoadThreadCommentsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadThreadCommentsQuery, LoadThreadCommentsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadThreadCommentsQueryResponse> Handle(LoadThreadCommentsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.ThreadsRepository.LoadThreadComments(request.AccountId, request.ThreadId, request.Username, request.Keyword, request.Sort, request.Pointer ?? 0, request.Size ?? 5);

            response.Username = request.Username;
            response.Keyword = request.Keyword;

            response.ThreadId = request.ThreadId;
            response.Sort = request.Sort ?? Domain.Enums.SortTypes.Most_Recent;
            response.Pointer = response.Comments == null || response.Comments.Count < (request.Size ?? 5) ? -1 : (request.Pointer ?? 0) + (request.Size ?? 5);
            response.Size = request.Size ?? 5;
            response.More = response.Comments != null && response.Comments.Count > (request.Size ?? 5);
            response.Comments = response.Comments?.Take(request.Size ?? 5).ToList();

            return response;
        }
    }

}
