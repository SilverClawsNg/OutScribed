using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using OutScribed.Domain.Models.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.UserManagement.Commands.CreateAccountEmail
{
    public class CreateAccountEmailCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<CreateAccountEmailCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<bool>> Handle(CreateAccountEmailCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new CreateAccountEmailCommandValidator();
           
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

            //Get user with either email address or phone number. return error if not found
            TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByEmailAddress(request.EmailAddress);

            if (user is null || user.Verified == false)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
             Title: "User Not Found",
           Description: $"User with email address: '{request.EmailAddress}' was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            if(await _unitOfWork.UserRepository.CheckIfUsernameExists(request.Username))
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "Duplicate Username",
          Description: $"The username: '{request.Username}' has already been taken.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

                //upgrade guest
                var result = Account.Create(
                ContactTypes.Email,
                request.EmailAddress,
                null,
                request.Username,
                request.Password);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Account>().Add(result.Value);

            //Remove user from repository
            _unitOfWork.RepositoryFactory<TempUser>().Remove(user);

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
