using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.TempUserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.TempUserManagement.Commands.SendRegistrationOTPPhone;

public class SendRegistrationOTPPhoneCommandHandler(IUnitOfWork unitOfWork, IMailSender mailsender,
    IErrorLogger errorLogger)
    : IRequestHandler<SendRegistrationOTPPhoneCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMailSender _mailSender = mailsender;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public async Task<Result<bool>> Handle(SendRegistrationOTPPhoneCommand request, CancellationToken cancellationToken)
    {

        //Validate request
        var validator = new SendRegistrationOTPPhoneCommandValidator();
        
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

        int password;

        if(await _unitOfWork.UserRepository.CheckIfPhoneNumberExists(request.PhoneNumber))
        {
            var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                           Title: "Duplicate Phone Number",
                                           Description: "Phone number is already associated to an existing account.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }

        //Get user with either email address or phone number. return error if not found
        TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByPhoneNumber(request.PhoneNumber);

        if(user is null)
        {
            //Build user entity
            var result = TempUser.Create(ContactTypes.Telephone, null, request.PhoneNumber);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            password = result.Value.Otp.Password;

            //Add user to repository
            _unitOfWork.RepositoryFactory<TempUser>().Add(result.Value);
        }
        else
        {
            if (user.DoNotResendOtp && DateTime.UtcNow.Subtract(user.Otp.Date).Minutes < 30)
            {
                var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                             Title: "Too Many OTP Sends",
                                             Description: "Too many one-time passwords has been resent. Wait at least 30 minutes before trying again.");

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
        }

        try
        {
            //Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //enqueue email
            //BackgroundJob.Enqueue(() => _mailSender.SendEmailVerificationOtpMail(request.EmailAddress, password));

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
