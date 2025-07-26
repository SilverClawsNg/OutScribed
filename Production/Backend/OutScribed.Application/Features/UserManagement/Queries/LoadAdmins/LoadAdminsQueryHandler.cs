using OutScribed.Application.Repositories;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadAdmins
{
    public class LoadAdminsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadAdminsQuery, LoadAdminsQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadAdminsQueryResponse> Handle(LoadAdminsQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.UserRepository.LoadAllAdmins(request.Role, request.Country, request.Sort, request.Username, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Username = request.Username;
            response.Role = request.Role;
            response.Country = request.Country;

            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Admins is null ? 0 : response.Admins.Count) > (request.Size ?? 20);

            var posts = response.Admins?.Take(request.Size ?? 20).ToList();
            response.Admins = posts;

            return response;
        }
    }

}
