using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.ThreadsManagement;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadPhoto
{
    public class UpdateThreadPhotoCommandHandler(IUnitOfWork unitOfWork, 
        IErrorLogger errorLogger, IFileHandler fileHandler, IWebHostEnvironment webHostEnvironment)
        : IRequestHandler<UpdateThreadPhotoCommand, Result<UpdateThreadPhotoResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<Result<UpdateThreadPhotoResponse>> Handle(UpdateThreadPhotoCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new UpdateThreadPhotoCommandValidator();
           
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

            if (request.Base64String == null)
            {

                var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                          Title: "Image Not Found",
                                          Description: $"Image was not found.");

                _errorLogger.LogError(errorResponse.Description);
                return errorResponse;

            }


            var oldPhoto = thread.PhotoUrl;

            string displayPhoto = string.Format("outscribed_{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(".jpg"));

            string displayPhotoPath = string.Format("outscribed/threads/{0}", displayPhoto);

            var photoResult = await _fileHandler.SaveFileAsync(request.Base64String, displayPhotoPath);

            if (photoResult.Error is not null)
            {

                _errorLogger.LogError(photoResult.Error.Description);

                return photoResult.Error;

            }


            //upgrade guest
            var result = thread.UpdatePhotoUrl(
                displayPhoto);

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

                if (oldPhoto != null)
                {
                    var deleteResult = await _fileHandler.DeleteFileAsync(string.Format("outscribed/threads/{0}", oldPhoto));

                    if (deleteResult.Error is not null)
                    {

                        _errorLogger.LogError(deleteResult.Error.Description);

                    }

                }

                return new UpdateThreadPhotoResponse() 
                { 
                    PhotoUrl = thread.PhotoUrl!.Value 
                };


            }
            catch (Exception ex)
            {

                var deleteResult = await _fileHandler.DeleteFileAsync(displayPhotoPath);

                if (deleteResult.Error is not null)
                {

                    _errorLogger.LogError(deleteResult.Error.Description);

                }

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);

            }

        }
    }
}
