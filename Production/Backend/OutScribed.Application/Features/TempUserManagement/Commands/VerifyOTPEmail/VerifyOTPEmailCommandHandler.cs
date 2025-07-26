using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.TempUserManagement.Commands.VerifyOTPEmail;

public class VerifyOTPEmailByPhoneNumberCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
    : IRequestHandler<VerifyOTPEmailCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public async Task<Result<bool>> Handle(VerifyOTPEmailCommand request, CancellationToken cancellationToken)
    {
        //Validate request
        var validator = new VerifyOTPEmailCommandValidator();
        
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

        if (user is null)
        {
            var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "User Not Found",
          Description: $"User with email address: '{request.EmailAddress}' was not found.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }

        if (user.Otp.Password != request.Otp)
        {
            var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "Password Incorrect",
          Description: $"One time password: '{request.Otp}' is incorrect.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }

        //validate email address
        var result = user.SetVerified();

        if (result.Error is not null)
        {
            _errorLogger.LogError(result.Error.Description);

            return result;
        }

        //Add user to repository
        _unitOfWork.RepositoryFactory<TempUser>().Update(user);

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
