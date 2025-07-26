using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadDetails;

public class UpdateThreadDetailsCommandValidator : AbstractValidator<UpdateThreadDetailsCommand>
{
    public UpdateThreadDetailsCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Thread is required.")
      .NotEmpty().WithMessage("Thread is required.");

        RuleFor(c => c.Details)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Details is required")
     .NotEmpty().WithMessage("Details is required")
     .MaximumLength(32768).WithMessage("Details must not exceed 32768 chars.");

    }
}
