using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.UserManagement.Entities;

namespace OutScribed.Application.Features.UserManagement.Commands.ResetPassword
{

    public class ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<ResetPasswordCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new ResetPasswordCommandValidator();
           
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

            //Get account with either email address or phone number. 
            Account? account = await _unitOfWork.UserRepository.GetAccountByUsername(request.Username);

            //Checks if account exists
            if (account is null)
            {
                var errorResult = new Error(Code: StatusCodes.Status404NotFound,
                                                           Title: "User Not Found",
                                                           Description: $"User with accountname: '{request.Username}' was not found.");

                _errorLogger.LogError(errorResult.Description);

                return errorResult;
            }

            //change the default password
            var result = account.ResetPassword(request.Password, request.Otp);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add account to repository
            _unitOfWork.RepositoryFactory<Account>().Update(account);

            try
            {
                //Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //Return success
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
