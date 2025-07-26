using FluentValidation;

namespace OutScribed.Application.Features.UserManagement.Commands.CreateAccountPhone;

public class CreateAccountPhoneCommandValidator : AbstractValidator<CreateAccountPhoneCommand>
{
    public CreateAccountPhoneCommandValidator()
    {

        RuleFor(c => c.Username)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Username is required")
     .NotEmpty().WithMessage("Username is required")
     .MaximumLength(16).WithMessage("Userame must not exceed 16 chars.");

        RuleFor(c => c.PhoneNumber)
                         .Cascade(CascadeMode.Stop)
                         .NotEmpty().WithMessage("Phone number is required")
                         .MaximumLength(24).WithMessage("Phone number must not exceed 24 chars.");

        RuleFor(c => c.Password)
           .NotNull().WithMessage("New password is required.")
           .NotEmpty().WithMessage("New password is required")
           .MinimumLength(8).WithMessage("Password must be at least 8 chars.");

    }
}
