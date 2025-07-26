using FluentValidation;

namespace Backend.Application.Features.UserManagement.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {

        public AssignRoleCommandValidator()
        {

            RuleFor(c => c.Role)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("No role title was selected")
          .NotEmpty().WithMessage("No role title was selected")
          .IsInEnum().WithMessage("Invalid role title selected");

            RuleFor(c => c.AccountId)
          .Cascade(CascadeMode.Stop)
         .NotNull().WithMessage("User is required")
         .NotEmpty().WithMessage("User is required");

        }
    }
}
