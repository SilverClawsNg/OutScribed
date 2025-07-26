using Backend.Application.Interfaces;
using Backend.Application.Repositories;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.TalesManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleDetails
{
    public class UpdateTaleDetailsCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<UpdateTaleDetailsCommand, Result<UpdateTaleDetailsResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<UpdateTaleDetailsResponse>> Handle(UpdateTaleDetailsCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new UpdateTaleDetailsCommandValidator();
           
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
            var result = tale.UpdateDetails(
                request.Details);

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

                return new UpdateTaleDetailsResponse()
                {
                    Details = tale.Details!.Value
                };

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
