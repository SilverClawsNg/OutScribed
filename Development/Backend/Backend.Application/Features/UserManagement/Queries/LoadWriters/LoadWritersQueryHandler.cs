using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.UserManagement.Queries.LoadWriters
{
    public class LoadWritersQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadWritersQuery, LoadWritersQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadWritersQueryResponse> Handle(LoadWritersQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.UserRepository.LoadAllWriters(request.AccountId, request.Country, request.Sort, request.Username, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Username = request.Username;
            response.Country = request.Country;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Writers is null ? 0 : response.Writers.Count) > (request.Size ?? 20);

            var posts = response.Writers?.Take(request.Size ?? 20).ToList();
            response.Writers = posts;

            return response;
        }
    }

}
