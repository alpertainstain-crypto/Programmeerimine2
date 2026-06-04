using FluentValidation;

namespace KooliProjekt.Application.Features.Availability
{
    public class SaveAvailabilityCommandValidator : AbstractValidator<SaveAvailabilityCommand>
    {
        public SaveAvailabilityCommandValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters long")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
        }
    }
}
