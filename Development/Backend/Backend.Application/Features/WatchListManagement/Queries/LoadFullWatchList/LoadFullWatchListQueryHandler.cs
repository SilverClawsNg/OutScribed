using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchList
{
    public class LoadFullWatchListQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadFullWatchListQuery, LoadFullWatchListQueryResponse?>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadFullWatchListQueryResponse?> Handle(LoadFullWatchListQuery request, CancellationToken cancellationToken)
        {

            return await _unitOfWork.WatchListRepository.LoadFullWatchList(request.Id, request.AccountId);

        }
    }

}
