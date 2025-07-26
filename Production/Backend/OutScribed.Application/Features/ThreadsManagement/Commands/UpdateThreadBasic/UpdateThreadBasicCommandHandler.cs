using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.ThreadsManagement;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadBasic
{
    public class UpdateThreadBasicCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<UpdateThreadBasicCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<bool>> Handle(UpdateThreadBasicCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new UpdateThreadBasicCommandValidator();
           
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

            //Get thread
            Threads? thread = await _unitOfWork.ThreadsRepository.GetThreadById(request.Id);

            //Checks if profile exists
            if (thread is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Thread Not Found",
                                              Description: $"Thread with Id: '{request.Id}' was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            if (thread.ThreaderId != request.AccountId)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Unauthorized Operation",
                                              Description: $"Account not authorized to perform current operation.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            //upgrade guest
            var result = thread.UpdateBasic(
                request.Title,
                request.Category);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Threads>().Update(thread);

            try
            {
                //Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;

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
