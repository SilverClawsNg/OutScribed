using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.AddThreadAddendum;

public class AddThreadAddendumCommandValidator : AbstractValidator<AddThreadAddendumCommand>
{
    public AddThreadAddendumCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Thread is required.")
      .NotEmpty().WithMessage("Thread is required.");

        RuleFor(c => c.Details)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Details is required")
     .NotEmpty().WithMessage("Details is required")
     .MaximumLength(4096).WithMessage("Details must not exceed 4096 chars.");

    }
}
