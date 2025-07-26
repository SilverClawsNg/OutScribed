using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.ThreadsManagement;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadPhoto
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

            string displayPhoto = string.Format("atlantistales_{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(".jpg"));

            var displayPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("images/threads/{0}", displayPhoto));

            await _fileHandler.SaveFileAsync(request.Base64String, displayPhotoPath);


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

                    var oldPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("images/tales/{0}", oldPhoto));

                    _fileHandler.DeleteFile(oldPhotoPath);

                }
               

                return new UpdateThreadPhotoResponse() 
                { 
                    PhotoUrl = thread.PhotoUrl!.Value 
                };


            }
            catch (Exception ex)
            {

                _fileHandler.DeleteFile(displayPhotoPath);

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);

            }

        }
    }
}
