using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.TempUserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.TempUserManagement.Commands.VerifyOTPPhone;

public class VerifyOTPPhoneByPhoneNumberCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
    : IRequestHandler<VerifyOTPPhoneCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public async Task<Result<bool>> Handle(VerifyOTPPhoneCommand request, CancellationToken cancellationToken)
    {
        //Validate request
        var validator = new VerifyOTPPhoneCommandValidator();
        
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
        TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByPhoneNumber(request.PhoneNumber);

        if (user is null)
        {
            var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "User Not Found",
          Description: $"User with phone number: '{request.PhoneNumber}' was not found.");

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
