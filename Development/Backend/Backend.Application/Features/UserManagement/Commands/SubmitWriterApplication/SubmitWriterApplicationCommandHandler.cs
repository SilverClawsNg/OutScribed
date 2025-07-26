using Backend.Domain.Exceptions;
using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Models.UserManagement.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Backend.Application.Features.UserManagement.Commands.SubmitWriterApplication
{
    public class SubmitWriterApplicationCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger, 
        IFileHandler fileHandler, IWebHostEnvironment webHostEnvironment)
        : IRequestHandler<SubmitWriterApplicationCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

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


            var application = string.Format("atlantistales_{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(".pdf"));

            var applicationPath = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("documents/applications/{0}", application));

            await _fileHandler.SaveFileAsync(request.Base64String, applicationPath);

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

                if (applicationPath != null)
                    _fileHandler.DeleteFile(applicationPath);

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);
            }

        }

    }

}
