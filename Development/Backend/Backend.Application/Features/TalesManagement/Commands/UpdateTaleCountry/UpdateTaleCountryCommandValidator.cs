using FluentValidation;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleCountry;

public class UpdateTaleCountryCommandValidator : AbstractValidator<UpdateTaleCountryCommand>
{
    public UpdateTaleCountryCommandValidator()
    {

        RuleFor(c => c.Id)
      .Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tale is required.")
      .NotEmpty().WithMessage("Tale is required.");

        RuleFor(c => c.Country)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Country is required.")
          .NotEmpty().WithMessage("Country is required.")
          .IsInEnum().WithMessage("Invalid country.");

    }
}
