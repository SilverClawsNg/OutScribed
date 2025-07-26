using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Domain.Models.UserManagement.Entities;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUsernameProfile
{
    public class LoadUsernameProfileQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadUsernameProfileQuery, LoadUsernameProfileQueryResponse?>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadUsernameProfileQueryResponse?> Handle(LoadUsernameProfileQuery request, CancellationToken cancellationToken)
        {

            var response =  await _unitOfWork.UserRepository.LoadUsernameProfile(request.Id, request.AccountId);

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
