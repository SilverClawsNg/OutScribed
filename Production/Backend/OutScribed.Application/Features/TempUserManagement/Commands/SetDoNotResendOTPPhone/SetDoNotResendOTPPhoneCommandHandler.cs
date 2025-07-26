using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.TempUserManagement.Entities;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPPhone
{

    public class SetDoNotResendOTPPhoneHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger, 
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<SetDoNotResendOTPPhoneCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<bool>> Handle(SetDoNotResendOTPPhoneCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new SetDoNotResendOTPPhoneCommandValidator();
            
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
            TempUser? user = await _unitOfWork.TempUserRepository.GetTempUserByPhoneNumber(request.PhoneNumber);

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
