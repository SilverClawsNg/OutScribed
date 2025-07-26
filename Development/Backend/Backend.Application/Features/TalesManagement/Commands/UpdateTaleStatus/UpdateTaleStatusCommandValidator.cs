using FluentValidation;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleStatus;

public class UpdateTaleStatusCommandValidator : AbstractValidator<UpdateTaleStatusCommand>
{
    public UpdateTaleStatusCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tale is required.")
      .NotEmpty().WithMessage("Tale is required.");

        RuleFor(c => c.Status)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Status is required.")
          .NotEmpty().WithMessage("Status is required.")
          .IsInEnum().WithMessage("Invalid status.");

        RuleFor(c => c.Reasons)
   .MaximumLength(1024).WithMessage("Reasons must not exceed 1024 chars.");

    }
}
