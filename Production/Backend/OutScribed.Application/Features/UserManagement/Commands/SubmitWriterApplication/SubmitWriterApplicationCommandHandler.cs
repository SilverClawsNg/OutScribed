using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.UserManagement.Entities;

namespace OutScribed.Application.Features.UserManagement.Commands.SubmitWriterApplication
{
    public class SubmitWriterApplicationCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger, 
        IFileHandler fileHandler)
        : IRequestHandler<SubmitWriterApplicationCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;

        public async Task<Result<bool>> Handle(SubmitWriterApplicationCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new SubmitWriterApplicationCommandValidator();
           
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


            string application = string.Format("outscribed_{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(".pdf"));

            string applicationPath = string.Format("outscribed/applications/{0}", application);

            var applicationResult = await _fileHandler.SaveFileAsync(request.Base64String, applicationPath);

            if (applicationResult.Error is not null)
            {

                _errorLogger.LogError(applicationResult.Error.Description);

                return applicationResult.Error;

            }

            //update display photo
            var result = account.CreateWriterApplication(
                request.Address, 
                request.Country,
                application);

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

                //Return success
                return true;

            }
            catch (Exception ex)
            {

              
                var deleteResult = await _fileHandler.DeleteFileAsync(applicationPath);

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
