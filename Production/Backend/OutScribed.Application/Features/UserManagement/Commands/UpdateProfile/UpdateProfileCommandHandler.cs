using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.UserManagement.Entities;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger,
        IFileHandler fileHandler)
        : IRequestHandler<UpdateProfileCommand, Result<UpdateProfileResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;

        public async Task<Result<UpdateProfileResponse>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new UpdateProfileCommandValidator();
           
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

            //Get profile with Username. return error if not found
            Account? account = await _unitOfWork.UserRepository.GetAccountById(request.AccountId);

            //Checks if profile exists
            if (account is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Account Not Found",
                                              Description: $"Account with Id: '{request.AccountId}' was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            var oldPhoto = account.Profile?.PhotoUrl;

            string? displayPhoto = null;

            string? displayPhotoPath = null;


            if (request.Base64String != null)
            {

                displayPhoto = string.Format("outscribed_{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(".jpg"));

                displayPhotoPath = string.Format("outscribed/users/{0}", displayPhoto);

                var photoResult = await _fileHandler.SaveFileAsync(request.Base64String, displayPhotoPath);

                if (photoResult.Error is not null)
                {

                    _errorLogger.LogError(photoResult.Error.Description);

                    return photoResult.Error;

                }

            }


            //update display photo
            var result = account.UpdateProfile(
                request.Title, 
                request.Bio, 
                request.EmailAddress,
                request.PhoneNumber,
                request.IsHidden,
                displayPhoto
                );

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Account>().Update(account);

            //Save changes
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (oldPhoto != null)
                {
                    var deleteResult = await _fileHandler.DeleteFileAsync(string.Format("outscribed/users/{0}", oldPhoto));

                    if (deleteResult.Error is not null)
                    {

                        _errorLogger.LogError(deleteResult.Error.Description);

                    }

                }

                //Return success
                return new UpdateProfileResponse
                {
                    DisplayPhoto = displayPhoto
                };

            }
            catch (Exception ex)
            {

                if (displayPhotoPath != null)
                {
                    var deleteResult = await _fileHandler.DeleteFileAsync(displayPhotoPath);

                    if (deleteResult.Error is not null)
                    {

                        _errorLogger.LogError(deleteResult.Error.Description);

                    }
                }

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);
            }

        }

    }

}
