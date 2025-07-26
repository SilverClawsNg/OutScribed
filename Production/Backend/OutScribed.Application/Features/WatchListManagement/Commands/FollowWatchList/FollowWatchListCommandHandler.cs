using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.WatchListManagement.Entities;

namespace OutScribed.Application.Features.WatchListManagement.Commands.FollowWatchList
{
    public class FollowWatchListCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<FollowWatchListCommand, Result<FollowWatchListResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
      
        public async Task<Result<FollowWatchListResponse>> Handle(FollowWatchListCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new FollowWatchListCommandValidator();
           
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (validatorResult is not null && validatorResult.IsValid == false)
            {
                var errors = string.Join(". ", (validatorResult.Errors.Select(x => x.ErrorMessage).ToList()));

                var errorResponse = new Error(Code: StatusCodes.Status500InternalServerError,
                              Title: "Validation Errors",
                              Description: $"The following errors occured: '{errors}'.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;

            }

            WatchList? watchList = await _unitOfWork.WatchListRepository.GetWatchListById(request.WatchListId);

            //Checks if watchList exists
            if (watchList is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "WatchList Not Found",
                                              Description: $"WatchList with Id: '{request.WatchListId}' was not found.");

                _errorLogger.LogError($"WatchList with Id: '{request.WatchListId}' was not found.");

                return errorResponse;
            }

            //save comment
            var result = watchList.SaveFollow(request.AccountId, request.Option);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<WatchList>().Update(watchList);

            //Save changes
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //Return success
                return new FollowWatchListResponse()
                {
                    Counts = result.Value
                };

            }
            catch (Exception ex)
            {

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);
            }

        }

    }

}
