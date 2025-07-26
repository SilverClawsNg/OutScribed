using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Domain.Models.UserManagement.Entities;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.UserManagement.Commands.RefreshToken
{

    public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger,
        IJwtProvider jwtProvider) 
        : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new RefreshTokenCommandValidator();
            
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (validatorResult != null && validatorResult.IsValid == false)
            {
                var errors = string.Join(". ", (validatorResult.Errors.Select(x => x.ErrorMessage).ToList()));

                var errorResponse = new Error(Code: StatusCodes.Status500InternalServerError,
                               Title: "Validation Errors",
                               Description: $"The following errors occured: '{errors}'.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            //Get account with valid refresh token
            Account? account = await _unitOfWork.UserRepository.GetValidAccountFromRefreshToken(request.Token);

            //Check if token is valid
            if(account is null)
            {

                //Return failure
                return new RefreshTokenResponse
                {
                    IsSuccessful = false,
                    RefreshToken = null,
                    Token = null
                };
            }

            //Create a new refresh token
            var result = account.UpdateRefreshToken();

            if (result.Error != null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add account to repository
            _unitOfWork.RepositoryFactory<Account>().Update(account);

            //Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Issue JWT token
            var token = _jwtProvider.Generate(account);

            //Return success
            return new RefreshTokenResponse
            {
                IsSuccessful = true,
                RefreshToken = result.Value,
                Token = token
            };

        }

    }

}
