using OutScribed.Application.Repositories;
using OutScribed.Domain.Models.UserManagement.Entities;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.LogoutUser
{

    public class LogoutUserHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LogoutUserCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {

            if(request.RefreshToken != null)
            {
                //Get user with username
                Account? account = await _unitOfWork.UserRepository.GetUserFromRefreshToken(request.RefreshToken);

                if (account is not null)
                {
                    account.RemoveRefreshToken();

                    //Add user to repository
                    _unitOfWork.RepositoryFactory<Account>().Update(account);

                    //Save changes
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
           

            return Unit.Value;
        }

    }

}
