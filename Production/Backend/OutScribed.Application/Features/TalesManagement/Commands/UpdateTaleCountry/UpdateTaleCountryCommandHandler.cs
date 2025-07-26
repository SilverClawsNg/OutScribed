using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TalesManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleCountry
{
    public class UpdateTaleCountryCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<UpdateTaleCountryCommand, Result<bool>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<bool>> Handle(UpdateTaleCountryCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new UpdateTaleCountryCommandValidator();
           
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

            //Get tale
            Tale? tale = await _unitOfWork.TaleRepository.GetTaleById(request.Id);

            //Checks if profile exists
            if (tale is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Tale Not Found",
                                              Description: $"Tale with Id: '{request.Id}' was not found.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            //upgrade guest
            var result = tale.UpdateCountry(
                request.Country);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Tale>().Update(tale);

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
