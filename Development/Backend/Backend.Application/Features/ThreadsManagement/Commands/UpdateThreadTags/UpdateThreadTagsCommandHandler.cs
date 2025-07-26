using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.ThreadsManagement;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadTags
{
    public class UpdateThreadTagsCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<UpdateThreadTagsCommand, Result<UpdateThreadTagsResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<UpdateThreadTagsResponse>> Handle(UpdateThreadTagsCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new UpdateThreadTagsCommandValidator();
           
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

            //Get draft
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
            var result = thread.SaveTags(
                request.Tags);

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

                return new UpdateThreadTagsResponse()
                {
                    Tags = await _unitOfWork.ThreadsRepository.GetThreadTags(request.Id)
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
