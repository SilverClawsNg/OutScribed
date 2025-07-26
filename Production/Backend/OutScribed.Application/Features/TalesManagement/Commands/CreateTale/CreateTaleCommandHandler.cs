using OutScribed.Application.Interfaces;
using OutScribed.Application.Repositories;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.TalesManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<CreateTaleCommand, Result<CreateTaleResponse?>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;

        public async Task<Result<CreateTaleResponse?>> Handle(CreateTaleCommand request, CancellationToken cancellationToken)
        {
            //Validate request
            var validator = new CreateTaleCommandValidator();
           
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

                //upgrade guest
                var result = Tale.Create(
                request.AdminId,
                request.Title,
                request.Category);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Tale>().Add(result.Value);

            try
            {
                //Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new CreateTaleResponse()
                {
                    Tale = await _unitOfWork.TaleRepository.LoadCreateTaleResponse(result.Value.Id)
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
