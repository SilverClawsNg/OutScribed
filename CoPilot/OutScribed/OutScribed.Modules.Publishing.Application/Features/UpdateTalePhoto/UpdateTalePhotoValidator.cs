using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTalePhoto
{
    public class UpdateTalePhotoValidator : Validator<UpdateTalePhotoRequest>
    {
        public UpdateTalePhotoValidator()
        {

            RuleFor(c => c.Id)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Required parameter is missing.")
                   .NotEmpty().WithMessage("Required parameter is missing.");

            RuleFor(c => c.Base64String)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Photo is required")
                   .NotEmpty().WithMessage("Photo is required");
        }
    }
}
