using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Hangfire;

namespace OutScribed.Application.Features.UserManagement.Commands.ResendRecoveryOTP;

public class ResendRecoveryOTPCommandHandler(IUnitOfWork unitOfWork, IMailSender mailsender,
    IErrorLogger errorLogger)
    : IRequestHandler<ResendRecoveryOTPCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMailSender _mailSender = mailsender;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public async Task<Result<bool>> Handle(ResendRecoveryOTPCommand request, CancellationToken cancellationToken)
    {

        //Validate request
        var validator = new ResendRecoveryOTPCommandValidator();

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

        //Get account with either email address or phone number. return error if not found
        Account? account = await _unitOfWork.UserRepository.GetAccountByUsername(request.Username);

        //if account is null, return success to disallow email or phone number surfing
        if (account is null)
        {
            var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
            Title: "User Not Found",
          Description: $"User with phone number: '{request.Username}' was not found.");

            _errorLogger.LogError(errorResponse.Description);

            return errorResponse;
        }


        var result = account.ResendOtp();

        //Add account to repository
        _unitOfWork.RepositoryFactory<Account>().Update(account);

        try
        {
            //Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (account.EmailAddress != null)
            {
                //enqueue email
                BackgroundJob.Enqueue(() => _mailSender.SendResendPasswordRecoveryOtpMail(account.EmailAddress.Value, result.Value));

            }

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
