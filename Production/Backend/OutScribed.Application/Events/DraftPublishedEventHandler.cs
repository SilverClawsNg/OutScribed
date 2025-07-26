using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Events;
using OutScribed.Domain.Models.TalesManagement.Entities;
using MediatR;

namespace OutScribed.Application.Events
{
    public class DraftPublishedEventHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
      : INotificationHandler<DraftPublishedEvent>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task Handle(DraftPublishedEvent notification, CancellationToken cancellationToken)
        {

           

            var followers = await _unitOfWork.UserRepository.GetMailingList(notification.AccountId);



            if(followers != null && followers.Count > 0)
            {



                //sends emails to followers on mailing list
            }

      
            await Task.CompletedTask;
        }
    }

}
