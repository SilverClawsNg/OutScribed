using Backend.Application.Repositories;
using Backend.Domain.Models.UserManagement.Entities;
using MediatR;

namespace Backend.Application.Features.UserManagement.Queries.LoadActivities
{
    public class LoadActivitiesQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadActivitiesQuery, LoadActivitiesQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadActivitiesQueryResponse> Handle(LoadActivitiesQuery request, CancellationToken cancellationToken)
        {

            var response = await _unitOfWork.UserRepository.LoadActivities(request.AccountId, request.Type, request.HasRead, request.Keyword, request.Sort, request.Pointer ?? 0, request.Size ?? 20);

            response.Pointer = request.Pointer ?? 0;
            response.Size = request.Size ?? 20;
            response.Sort = request.Sort;

            response.Type = request.Type;
            response.Keyword = request.Keyword;
            response.HasRead = request.HasRead;


            response.Previous = request.Pointer != null && request.Pointer > 0;
            response.Next = (response.Activities is null ? 0 : response.Activities.Count) > (request.Size ?? 20);

            var activities = response.Activities?.Take(request.Size ?? 20).ToList();

            if (activities != null && activities.Count != 0)
            {
                Account? account = await _unitOfWork.UserRepository.GetAccountById(request.AccountId);

                if (account is not null)
                {

                    foreach(var activity in activities)
                    {
                        account.MarkAsRead(activity.Id);

                    }


                    _unitOfWork.RepositoryFactory<Account>().Update(account);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
            }


            response.Activities = activities;

            return response;
        }
    }

}
