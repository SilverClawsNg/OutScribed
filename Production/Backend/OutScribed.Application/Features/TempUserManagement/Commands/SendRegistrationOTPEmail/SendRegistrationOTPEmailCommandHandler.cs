using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Hangfire;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SendRegistrationOTPEmail;

public class SendRegistrationOTPEmailCommandHandler(IUnitOfWork unitOfWork, IMailSender mailsender,
    IErrorLogger errorLogger)
    : IRequestHandler<SendRegistrationOTPEmailCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMailSender _mailSender = mailsender;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public async Task<Result<bool>> Handle(SendRegistrationOTPEmailCommand request, CancellationToken cancellationToken)
    {

        //Validate request
        var validator = new SendRegistrationOTPEmailCommandValidator();

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

        if (await _unitOfWork.UserRepository.CheckIfEmailAddressExists(request.EmailAddress))
        {
            var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                           Title: "Duplicate Email Address",
                                           Description: "Email address is already associated to an existing account.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }

        //Get user with either email address or phone number. return error if not found
        TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByEmailAddress(request.EmailAddress);

        if (user is null)
        {
            //Build user entity
            var result = TempUser.Create(ContactTypes.Email, request.EmailAddress, null);

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
            //Add new otp to user
            var result = user.SendOtp();

            password = result.Value;

            //Add user to repository
            _unitOfWork.RepositoryFactory<TempUser>().Update(user);
        }

        try
        {
            //Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //enqueue email
            BackgroundJob.Enqueue(() => _mailSender.SendVerificationOtpMail(request.EmailAddress, password));

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
