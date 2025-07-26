using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.SubmitWriterApplication
{
    public class SubmitWriterApplicationCommandValidator : AbstractValidator<SubmitWriterApplicationCommand>
    {
        public SubmitWriterApplicationCommandValidator()
        {


            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Account is required.")
            .NotEmpty().WithMessage("Account is required.");

            RuleFor(c => c.Address)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Address is required")
        .NotEmpty().WithMessage("Address is required")
        .MaximumLength(128).WithMessage("Address must not exceed 128 chars.");

            RuleFor(c => c.Country)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Country is required.")
            .NotEmpty().WithMessage("Country is required.")
            .IsInEnum().WithMessage("Invalid country.");

            RuleFor(c => c.Base64String)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Application is required")
          .NotEmpty().WithMessage("Application is required");

        }
    }
}
