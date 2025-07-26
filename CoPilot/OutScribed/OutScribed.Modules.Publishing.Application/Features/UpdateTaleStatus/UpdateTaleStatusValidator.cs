using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleStatus
{
    public class UpdateTaleStatusValidator : Validator<UpdateTaleStatusRequest>
    {
        public UpdateTaleStatusValidator()
        {

            RuleFor(c => c.Id)
                      .Cascade(CascadeMode.Stop)
                      .NotNull().WithMessage("Required parameter is missing.")
                      .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Status)
                     .Cascade(CascadeMode.Stop)
                     .NotNull().WithMessage("Status is required.")
                     .NotEmpty().WithMessage("Status is required.")
                     .IsInEnum().WithMessage("Status is invalid.");

            RuleFor(c => c.Notes)
                   .Cascade(CascadeMode.Stop)
                   .Length(3, 2048).WithMessage("Notes should be between 3 and 2048 chars.");
        }
    }
}
