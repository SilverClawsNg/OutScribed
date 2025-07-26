using FluentValidation;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadTags;

public class UpdateThreadTagsCommandValidator : AbstractValidator<UpdateThreadTagsCommand>
{
    public UpdateThreadTagsCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Draft is required.")
      .NotEmpty().WithMessage("Draft is required.");

        RuleFor(c => c.Tags)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Tags is required")
     .NotEmpty().WithMessage("Tags is required")
     .MaximumLength(164).WithMessage("Tags must not exceed 164 chars.");

    }
}
