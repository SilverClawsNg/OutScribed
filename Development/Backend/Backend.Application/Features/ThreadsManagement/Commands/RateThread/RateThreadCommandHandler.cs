using Backend.Domain.Exceptions;
using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Models.ThreadsManagement;

namespace Backend.Application.Features.ThreadsManagement.Commands.RateThread
{
    public class RateThreadCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<RateThreadCommand, Result<RateThreadResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
       
        public async Task<Result<RateThreadResponse>> Handle(RateThreadCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new RateThreadCommandValidator();
            
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

            Threads? threads = await _unitOfWork.ThreadsRepository.GetThreadById(request.ThreadId);

            //Checks if threads exists
            if (threads is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Thread Not Found",
                                              Description: $"Thread with Id: '{request.ThreadId}' was not found.");

                _errorLogger.LogError($"Thread with Id: '{request.ThreadId}' was not found.");

                return errorResponse;
            }


            //save comment
            var result = threads.SaveRating(request.RateType, request.UserId);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Threads>().Update(threads);

            //Save changes
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //Return success
                return new RateThreadResponse()
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
