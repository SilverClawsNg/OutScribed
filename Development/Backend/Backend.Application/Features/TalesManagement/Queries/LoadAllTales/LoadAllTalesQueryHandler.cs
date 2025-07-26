using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadAllTales
{
    public class LoadAllTalesQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadAllTalesQuery, LoadAllTalesQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadAllTalesQueryResponse> Handle(LoadAllTalesQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.TaleRepository.LoadAllTales(request.AccountId, request.Category, request.Country, request.Username, request.Sort, request.WatchlistId, request.Tag, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Category = request.Category;
            response.Country = request.Country;
            response.Username = request.Username;
            response.Keyword = request.Keyword;
            response.WatchlistId = request.WatchlistId;

            response.Tag = request.Tag;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Tales is null ? 0 : response.Tales.Count) > (request.Size ?? 20);

            response.Tales = response.Tales?.Take(request.Size ?? 20).ToList();

            return response;
        }
    }

}
