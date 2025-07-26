using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadAllTaleDrafts
{
    public class LoadAllTaleDraftsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadAllTaleDraftsQuery, LoadAllTaleDraftsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadAllTaleDraftsQueryResponse> Handle(LoadAllTaleDraftsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.TaleRepository.LoadAllTaleDrafts(request.Status, request.Category, request.Country, request.Username, request.Sort, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Keyword = request.Keyword;
            response.Status = request.Status;
            response.Category = request.Category;
            response.Country = request.Country;
            response.Username = request.Username;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Tales is null ? 0 : response.Tales.Count) > (request.Size ?? 20);

            response.Tales = response.Tales?.Take(request.Size ?? 20).ToList();

            return response;
        }
    }

}
