using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.CreateAccountEmail;

public class CreateAccountEmailCommandValidator : AbstractValidator<CreateAccountEmailCommand>
{
    public CreateAccountEmailCommandValidator()
    {

        RuleFor(c => c.Username)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Username is required")
     .NotEmpty().WithMessage("Username is required")
     .MaximumLength(20).WithMessage("Userame must not exceed 20 chars.");

        RuleFor(c => c.EmailAddress)
    .Cascade(CascadeMode.Stop)
    .NotNull().WithMessage("Missing parameters")
    .NotEmpty().WithMessage("Missing parameters");

        RuleFor(c => c.Password)
           .NotNull().WithMessage("New password is required.")
           .NotEmpty().WithMessage("New password is required")
           .MinimumLength(8).WithMessage("Password must be at least 8 chars.");

    }
}
