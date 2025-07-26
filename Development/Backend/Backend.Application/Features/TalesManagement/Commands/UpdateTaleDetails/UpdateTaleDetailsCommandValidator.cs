using FluentValidation;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleDetails;

public class UpdateTaleDetailsCommandValidator : AbstractValidator<UpdateTaleDetailsCommand>
{
    public UpdateTaleDetailsCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tale is required.")
      .NotEmpty().WithMessage("Tale is required.");

        RuleFor(c => c.Details)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Details is required")
     .NotEmpty().WithMessage("Details is required")
     .MaximumLength(32768).WithMessage("Details must not exceed 32768 chars.");

    }
}
