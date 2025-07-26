using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.WatchListManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.WatchListManagement.Commands.UpdateWatchList
{
    public class UpdateWatchListCommandHandler(IUnitOfWork unitOfWork, 
        IErrorLogger errorLogger, IFileHandler fileHandler, IWebHostEnvironment webHostEnvironment)
        : IRequestHandler<UpdateWatchListCommand, Result<UpdateWatchListResponse?>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<Result<UpdateWatchListResponse?>> Handle(UpdateWatchListCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new UpdateWatchListCommandValidator();
           
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

            WatchList? watchList = await _unitOfWork.WatchListRepository.GetWatchListById(request.Id);

            //Checks if watchList exists
            if (watchList is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "WatchList Not Found",
                                              Description: $"WatchList with Id: '{request.Id}' was not found.");

                _errorLogger.LogError($"WatchList with Id: '{request.Id}' was not found.");

                return errorResponse;
            }

            //upgrade guest
            var result = watchList.Update(
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
            _unitOfWork.RepositoryFactory<WatchList>().Update(watchList);

            try
            {
                //Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new UpdateWatchListResponse()
                {
                    WatchList = await _unitOfWork.WatchListRepository.LoadWatchList(request.Id)
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
