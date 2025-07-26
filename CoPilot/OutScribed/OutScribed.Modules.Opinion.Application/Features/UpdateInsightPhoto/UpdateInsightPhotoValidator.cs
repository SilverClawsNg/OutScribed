using FastEndpoints;
using FluentValidation;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightPhoto
{
    public class UpdateInsightPhotoValidator : Validator<UpdateInsightPhotoRequest>
    {
        public UpdateInsightPhotoValidator()
        {
            RuleFor(c => c.Id)
                       .Cascade(CascadeMode.Stop)
                       .NotNull().WithMessage("Insight is required.")
                       .NotEmpty().WithMessage("Insight is required.");

            RuleFor(c => c.Base64String)
                   .Cascade(CascadeMode.Stop)
                   .NotNull().WithMessage("Photo is required")
                   .NotEmpty().WithMessage("Photo is required");
        }
    }
}
