using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.RateThread
{
    public class RateThreadCommandValidator : AbstractValidator<RateThreadCommand>
    {
        public RateThreadCommandValidator()
        {

            RuleFor(c => c.ThreadId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Thread is required.")
            .NotEmpty().WithMessage("Thread is required.");

            RuleFor(c => c.UserId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("User is required.")
            .NotEmpty().WithMessage("User is required.");

            RuleFor(c => c.RateType)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Type is required.")
          .NotEmpty().WithMessage("Type is required.")
          .IsInEnum().WithMessage("Invalid type.");
        }
    }
}
