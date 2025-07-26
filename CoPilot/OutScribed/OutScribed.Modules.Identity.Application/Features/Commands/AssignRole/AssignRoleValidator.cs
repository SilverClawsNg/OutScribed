using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.AssignRole
{
    public class AssignRoleValidator : Validator<AssignRoleRequest>
    {

        public AssignRoleValidator()
        {
            RuleFor(c => c.AccountId)
                         .Cascade(CascadeMode.Stop)
                         .NotNull().WithMessage("Account is required")
                         .NotEmpty().WithMessage("Account is required");

            RuleFor(c => c.Type)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Role type is required.")
                   .NotEmpty().WithMessage("Role type is required.")
                   .IsInEnum().WithMessage("Role type is invalid.");
        }

    }
}
