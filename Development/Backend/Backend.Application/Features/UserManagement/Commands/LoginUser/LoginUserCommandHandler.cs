using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.UserManagement.Entities;

namespace Backend.Application.Features.UserManagement.Commands.LoginUser
{

    public class LoginUserCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger,
        IJwtProvider jwtProvider, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new LoginUserCommandValidator();

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

            //Get account with username
            Account? account = await _unitOfWork.UserRepository.GetAccountByUsername(request.Username);

            //if account is null
            if (account is null)
            {

                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                             Title: "User Not Found",
                                             Description: $"User with Username: '{request.Username}' was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            var ipAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress;

            if (ipAddress is null)
            {

                var errorResponse = new Error(Code: StatusCodes.Status400BadRequest,
                                             Title: "IP Address Not Found",
                                             Description: $"Remote Ip Address was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            //Compare password and check if email address and phone number are verified.
            var result = account.ConfirmLogin(request.Password, ipAddress.ToString());

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Account>().Update(account);

            //Save changes
            try
            {
                //Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var response = await _unitOfWork.UserRepository.LoadLoginUserResponse(account.Id);

                //Issue JWT token

                if (response != null)
                {

                    var token = _jwtProvider.Generate(account);

                    response.IsSuccessful = true;

                    response.RefreshToken = result.Value;

                    response.Token = token;

                    return response;
                }
                else
                {
                    var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                            Title: "User Not Found",
                                            Description: $"User was not found");

                    _errorLogger.LogError(errorResponse.Description);

                    return errorResponse;
                }

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
