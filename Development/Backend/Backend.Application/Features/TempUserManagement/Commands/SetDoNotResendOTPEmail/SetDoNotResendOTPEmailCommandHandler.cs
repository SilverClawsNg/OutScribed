using Backend.Domain.Exceptions;
using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Models.TempUserManagement.Entities;

namespace Backend.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPEmail
{

    public class SetDoNotResendOTPEmailHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger, 
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<SetDoNotResendOTPEmailCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<bool>> Handle(SetDoNotResendOTPEmailCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new SetDoNotResendOTPEmailCommandValidator();
            
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

            //Get user with either email address or phone number. 
            TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByEmailAddress(request.EmailAddress);

            if (user is not null)
            {
                //Set account status to active
                var result = user.SetDoNotResendOtp();

                if (result.Error is not null)
                {
                    _errorLogger.LogError(result.Error.Description);

                    return result;
                }

                _unitOfWork.RepositoryFactory<TempUser>().Update(user);

                try
                {
                    //Save changes
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
                catch (Exception ex)
                {

                     _errorLogger.LogError(ex);

                    return new Error(Code: StatusCodes.Status500InternalServerError,
                                                  Title: "Database Error",
                                                  Description: ex.Message);

                }
            }
            
            return true;

        }

    }

}
