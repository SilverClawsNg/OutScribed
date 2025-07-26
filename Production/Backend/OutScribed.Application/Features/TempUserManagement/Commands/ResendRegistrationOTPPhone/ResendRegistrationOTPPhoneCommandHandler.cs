using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using OutScribed.Domain.Models.UserManagement.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPPhone;

public class ResendRegistrationOTPPhoneCommandHandler(IUnitOfWork unitOfWork, IMailSender mailsender,
    IErrorLogger errorLogger)
    : IRequestHandler<ResendRegistrationOTPPhoneCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMailSender _mailSender = mailsender;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public async Task<Result<bool>> Handle(ResendRegistrationOTPPhoneCommand request, CancellationToken cancellationToken)
    {

        //Validate request
        var validator = new ResendRegistrationOTPPhoneCommandValidator();
       
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

        int password;

        //Get user with either email address or phone number. return error if not found
        TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByPhoneNumber(request.PhoneNumber);

        //if user is null, return success to disallow email or phone number surfing
        if (user is null)
        {
            var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "User Not Found",
          Description: $"User with phone number: '{request.PhoneNumber}' was not found.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }
        else if (user.DoNotResendOtp && DateTime.UtcNow.Subtract(user.Otp.Date).Minutes < 30)
        {
            var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                         Title: "Too Many OTP Sends",
                                         Description: "Too many one-time passwords has been resent. Wait at least 30 minutes before trying again.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }
        else if (DateTime.UtcNow.Subtract(user.Otp.Date).Seconds < 90)
        {
            var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                             Title: "Too Many OTP Sends",
                                             Description: "Too many one-time passwords has been resent. Wait at least 2 minutes before trying again.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }
        else
        {
            //Add new otp to user
            var result = user.AddOtp();

            password = result.Value;

            //Add user to repository
            _unitOfWork.RepositoryFactory<TempUser>().Update(user);

        }

        try
        {
            //Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //enqueue email
            //BackgroundJob.Enqueue(() => _mailSender.ResendVerificationOtpMail(request.EmailAddress, password));

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
