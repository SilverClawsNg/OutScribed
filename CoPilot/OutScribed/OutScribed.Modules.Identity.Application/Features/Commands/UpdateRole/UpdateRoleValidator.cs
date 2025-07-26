using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateRole
{
    public class UpdateRoleValidator : Validator<UpdateRoleRequest>
    {
        public UpdateRoleValidator()
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

            RuleFor(c => c.IsActive)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Option is required")
                    .NotEmpty().WithMessage("Optipn is required");
        }
    }
}
