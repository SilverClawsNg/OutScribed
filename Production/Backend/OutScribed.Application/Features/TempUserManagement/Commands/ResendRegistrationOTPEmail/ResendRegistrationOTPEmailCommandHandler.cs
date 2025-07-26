using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Hangfire;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPEmail;

public class ResendRegistrationOTPEmailCommandHandler(IUnitOfWork unitOfWork, IMailSender mailsender,
    IErrorLogger errorLogger)
    : IRequestHandler<ResendRegistrationOTPEmailCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMailSender _mailSender = mailsender;
    private readonly IErrorLogger _errorLogger = errorLogger;


    public async Task<Result<bool>> Handle(ResendRegistrationOTPEmailCommand request, CancellationToken cancellationToken)
    {

        //Validate request
        var validator = new ResendRegistrationOTPEmailCommandValidator();

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
        TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByEmailAddress(request.EmailAddress);

        //if user is null, return success to disallow email or phone number surfing
        if (user is null)
        {
            var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "User Not Found",
          Description: $"User with email address: '{request.EmailAddress}' was not found.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }

        //Add new otp to user
        var result = user.ResendOtp();

        password = result.Value;

        //Add user to repository
        _unitOfWork.RepositoryFactory<TempUser>().Update(user);

        try
        {
            //Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //enqueue email
            BackgroundJob.Enqueue(() => _mailSender.SendResendVerificationOtpMail(request.EmailAddress, password));

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
