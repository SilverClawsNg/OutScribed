using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTaleLinks
{
    public class LoadTaleLinksQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadTaleLinksQuery, LoadTaleLinksQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadTaleLinksQueryResponse> Handle(LoadTaleLinksQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.TaleRepository.LoadTaleLinks(request.UserId, request.Sort, request.Keyword, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Keyword = request.Keyword;


            response.More = response.Tales != null && response.Tales.Count > (request.Size ?? 5);


            response.Tales = response.Tales?.Take(request.Size ?? 20).ToList();

            return response;
        }
    }

}
