using FluentValidation;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTalePhoto;

public class UpdateTalePhotoCommandValidator : AbstractValidator<UpdateTalePhotoCommand>
{
    public UpdateTalePhotoCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tale is required.")
      .NotEmpty().WithMessage("Tale is required.");

        RuleFor(c => c.Base64String)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Photo is required")
     .NotEmpty().WithMessage("Photo is required");

    }
}
