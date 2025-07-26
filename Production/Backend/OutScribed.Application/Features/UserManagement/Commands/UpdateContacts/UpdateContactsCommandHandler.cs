using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.UserManagement.Entities;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdateContacts
{
    public class UpdateIndividualBasicCommandHandler(IUnitOfWork unitOfWork, 
        IErrorLogger errorLogger)
        : IRequestHandler<UpdateContactsCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
       
        public async Task<Result<bool>> Handle(UpdateContactsCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new UpdateContactsCommandValidator();
           
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

            //Get profile with Username. return error if not found
            Account? account = await _unitOfWork.UserRepository.GetAccountById(request.AccountId);

            //Checks if profile exists
            if (account is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Account Not Found",
                                              Description: $"Account with Id: '{request.AccountId}' was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            var result = account.UpdateContact(request.ContactType, request.ContactValue);

            if (result.Error is not null)
            {

                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add profile to repository
            _unitOfWork.RepositoryFactory<Account>().Update(account);

            //Save changes

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

}
