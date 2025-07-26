using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Events;
using OutScribed.Domain.Models.UserManagement.Entities;
using MediatR;

namespace OutScribed.Application.Events
{
    public class ActivityUpdatedEventHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
     : INotificationHandler<ActivityUpdatedEvent>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task Handle(ActivityUpdatedEvent notification, CancellationToken cancellationToken)
        {
            //Get account with either email address or phone number. return error if not found
            var account = await _unitOfWork.UserRepository.GetAccountById(notification.ActorId);

            //Checks if account exists
            if (account is not null)
            {
                //add activity
                var result = account.AddActivity(notification.Details, notification.Type,
                    notification.ConstructorType, notification.Date);

                if (result.Error == null)
                {

                    //Add account to repository
                    _unitOfWork.RepositoryFactory<Account>().Update(account);

                }
                else
                {
                    //log error
                    _errorLogger.LogError(result.Error.Description);

                }
            }

            await Task.CompletedTask;

        }
    }

}
