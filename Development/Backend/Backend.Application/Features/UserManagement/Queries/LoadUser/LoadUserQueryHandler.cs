using Backend.Application.Repositories;
using Backend.Domain.Models.UserManagement.Entities;
using MediatR;

namespace Backend.Application.Features.UserManagement.Queries.LoadUser
{
    public class LoadUserQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadUserQuery, LoadUserQueryResponse?>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadUserQueryResponse?> Handle(LoadUserQuery request, CancellationToken cancellationToken)
        {

            var response =  await _unitOfWork.UserRepository.LoadUser(request.Id, request.AccountId);

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
