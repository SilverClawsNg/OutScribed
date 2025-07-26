using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.TalesManagement.Entities;

namespace Backend.Application.Features.TalesManagement.Commands.RateTaleComment
{
    public class RateTaleCommentCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<RateTaleCommentCommand, Result<RateTaleCommentResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
       
        public async Task<Result<RateTaleCommentResponse>> Handle(RateTaleCommentCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new RateTaleCommentCommandValidator();

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

            Tale? tale = await _unitOfWork.TaleRepository.GetTaleById(request.TaleId);

            //Checks if tale exists
            if (tale is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Tale Not Found",
                                              Description: $"Tale with Id: '{request.TaleId}' was not found.");

                _errorLogger.LogError($"Tale with Id: '{request.TaleId}' was not found.");

                return errorResponse;
            }

            //save comment
            var result = tale.SaveCommentRating(
                request.RateType, 
                request.AccountId, 
                request.CommentId);

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Tale>().Update(tale);

            //Save changes
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //Return success
                return new RateTaleCommentResponse()
                {
                    Counts = result.Value
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
