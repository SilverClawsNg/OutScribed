using Backend.Domain.Exceptions;
using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Models.ThreadsManagement;

namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThreadResponse
{
    public class CreateThreadResponseCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger,
        IFileHandler fileHandler, IWebHostEnvironment webHostEnvironment)
        : IRequestHandler<CreateThreadResponseCommand, Result<CreateThreadResponseResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<Result<CreateThreadResponseResponse>> Handle(CreateThreadResponseCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new CreateThreadResponseCommandValidator();
            
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
            var result = threads.SaveResponse(
                request.ParentId, 
                request.Details, 
                request.AccountId,
               request.CommentatorId,
                await _unitOfWork.UserRepository.GetUsernameById(request.AccountId));

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
                return new CreateThreadResponseResponse
                {
                    Comment = await _unitOfWork.ThreadsRepository.LoadThreadComment(request.AccountId, result.Value)
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
