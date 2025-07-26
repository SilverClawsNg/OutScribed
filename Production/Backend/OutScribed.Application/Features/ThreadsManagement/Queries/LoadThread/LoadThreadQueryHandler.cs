using OutScribed.Application.Repositories;
using OutScribed.Domain.Models.ThreadsManagement;
using OutScribed.Domain.Models.UserManagement.Entities;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThread
{
    public class LoadThreadQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadThreadQuery, LoadThreadQueryResponse?>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadThreadQueryResponse?> Handle(LoadThreadQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.ThreadsRepository.LoadThread(request.Url, request.AccountId);

            if (response != null)
            {
                Threads? thread = await _unitOfWork.ThreadsRepository.GetThreadById(response.Id);

                if (thread is not null)
                {
                    thread.UpdateViews();

                    _unitOfWork.RepositoryFactory<Threads>().Update(thread);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
            }

            return response;
        }
    }

}
