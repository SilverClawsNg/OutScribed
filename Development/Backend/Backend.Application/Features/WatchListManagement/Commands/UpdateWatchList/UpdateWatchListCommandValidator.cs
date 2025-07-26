using FluentValidation;

namespace Backend.Application.Features.WatchListManagement.Commands.UpdateWatchList;

public class UpdateWatchListCommandValidator : AbstractValidator<UpdateWatchListCommand>
{
    public UpdateWatchListCommandValidator()
    {


        RuleFor(c => c.Id)
 .Cascade(CascadeMode.Stop)
 .NotNull().WithMessage("Watchlist is required")
 .NotEmpty().WithMessage("Watchlist is required");

        RuleFor(c => c.Title)
     .Cascade(CascadeMode.Stop)
     .NotNull().WithMessage("Title is required")
     .NotEmpty().WithMessage("Title is required")
     .MaximumLength(128).WithMessage("Title must not exceed 128 chars.");

        RuleFor(c => c.Summary)
  .Cascade(CascadeMode.Stop)
  .NotNull().WithMessage("Summary is required")
  .NotEmpty().WithMessage("Summary is required")
  .MaximumLength(1024).WithMessage("Summary must not exceed 1024 chars.");

        RuleFor(c => c.SourceUrl)
  .Cascade(CascadeMode.Stop)
  .NotNull().WithMessage("Source url is required")
  .NotEmpty().WithMessage("Source url is required")
  .MaximumLength(128).WithMessage("Source url must not exceed 128 chars.");

        RuleFor(c => c.SourceText)
.Cascade(CascadeMode.Stop)
.NotNull().WithMessage("Source text is required")
.NotEmpty().WithMessage("Source text is required")
.MaximumLength(28).WithMessage("Source text must not exceed 28 chars.");

        RuleFor(c => c.Category)
          .Cascade(CascadeMode.Stop)
          .NotNull().WithMessage("Category is required.")
          .NotEmpty().WithMessage("Category is required.")
          .IsInEnum().WithMessage("Invalid category.");
    }
}
