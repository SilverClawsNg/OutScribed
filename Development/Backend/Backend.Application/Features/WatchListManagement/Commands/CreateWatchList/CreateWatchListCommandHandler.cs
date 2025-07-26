using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.WatchListManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.WatchListManagement.Commands.CreateWatchList
{
    public class CreateWatchListCommandHandler(IUnitOfWork unitOfWork, 
        IErrorLogger errorLogger)
        : IRequestHandler<CreateWatchListCommand, Result<CreateWatchListResponse?>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
       
        public async Task<Result<CreateWatchListResponse?>> Handle(CreateWatchListCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new CreateWatchListCommandValidator();
           
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (validatorResult is not null && validatorResult.IsValid == false)
            {
                var errors = string.Join(". ", validatorResult.Errors.Select(x => x.ErrorMessage).ToList());

                var errorResponse = new Error(Code: StatusCodes.Status500InternalServerError,
                          Title: "Validation Errors",
                          Description: $"The following errors occured: '{errors}'.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }


            //upgrade guest
            var result = WatchList.Create(
                request.AdminId,
                request.Title,
                request.Summary,
                request.SourceUrl,
                 request.SourceText,
                request.Category,
                request.Country);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<WatchList>().Add(result.Value);

            try
            {
                //Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new CreateWatchListResponse()
                {
                    WatchList = await _unitOfWork.WatchListRepository.LoadWatchList(result.Value.Id)
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
