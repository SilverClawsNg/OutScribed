using FluentValidation;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadPhoto;

public class UpdateThreadPhotoCommandValidator : AbstractValidator<UpdateThreadPhotoCommand>
{
    public UpdateThreadPhotoCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Thread is required.")
      .NotEmpty().WithMessage("Thread is required.");

        RuleFor(c => c.Base64String)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Photo is required")
     .NotEmpty().WithMessage("Photo is required");

    }
}
