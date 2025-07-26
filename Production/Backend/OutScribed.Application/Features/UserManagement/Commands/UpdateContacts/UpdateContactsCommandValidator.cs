using FluentValidation;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdateContacts
{
    public class UpdateContactsCommandValidator : AbstractValidator<UpdateContactsCommand>
    {
        public UpdateContactsCommandValidator()
        {

            RuleFor(c => c.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("User is required.")
            .NotEmpty().WithMessage("User is required.");

            RuleFor(c => c.ContactValue)
         .Cascade(CascadeMode.Stop)
         .NotNull().WithMessage("Contact value is required")
         .NotEmpty().WithMessage("Contact value is required")
         .MaximumLength(56).WithMessage("Contact value must not exceed 56 chars.");

            RuleFor(c => c.ContactType)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Contact type is required.")
            .NotEmpty().WithMessage("Contact type is required.")
            .IsInEnum().WithMessage("Invalid contact type.");

        }
    }
}
