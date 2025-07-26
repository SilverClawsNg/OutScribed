using FluentValidation;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleTags;

public class UpdateTaleTagsCommandValidator : AbstractValidator<UpdateTaleTagsCommand>
{
    public UpdateTaleTagsCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tale is required.")
      .NotEmpty().WithMessage("Tale is required.");

        RuleFor(c => c.Tags)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Tags is required")
     .NotEmpty().WithMessage("Tags is required")
     .MaximumLength(164).WithMessage("Tags must not exceed 164 chars.");

    }
}
