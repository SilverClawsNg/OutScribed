using OutScribed.Application.Repositories;
using OutScribed.Domain.Models.TalesManagement.Entities;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTale
{
    public class LoadTaleQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadTaleQuery, LoadTaleQueryResponse?>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadTaleQueryResponse?> Handle(LoadTaleQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.TaleRepository.LoadTale(request.Url, request.AccountId);

            if(response != null)
            {
                Tale? tale = await _unitOfWork.TaleRepository.GetTaleById(response.Id);

                if(tale is not null)
                {
                    tale.UpdateViews();

                    _unitOfWork.RepositoryFactory<Tale>().Update(tale);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
            }

            return response;
        }
    }

}
