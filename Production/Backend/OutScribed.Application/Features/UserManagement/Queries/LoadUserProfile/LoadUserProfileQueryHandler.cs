using OutScribed.Application.Repositories;
using OutScribed.Domain.Models.UserManagement.Entities;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUserProfile
{
    public class LoadUserProfileQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadUserProfileQuery, LoadUserProfileQueryResponse?>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadUserProfileQueryResponse?> Handle(LoadUserProfileQuery request, CancellationToken cancellationToken)
        {

            var response =  await _unitOfWork.UserRepository.LoadUserProfile(request.Id, request.AccountId);

            if (response != null)
            {
                Account? account = await _unitOfWork.UserRepository.GetAccountById(response.Id);

                if (account is not null)
                {
                    account.UpdateViews();

                    _unitOfWork.RepositoryFactory<Account>().Update(account);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
            }

            return response;
        }
    }

}
