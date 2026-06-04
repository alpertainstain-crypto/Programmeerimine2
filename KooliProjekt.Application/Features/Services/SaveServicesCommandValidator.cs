using FluentValidation;

namespace KooliProjekt.Application.Features.Services
{
    public class SaveServicesCommandValidator : AbstractValidator<SaveServicesCommand>
    {
        public SaveServicesCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required")
                .MinimumLength(2).WithMessage("Code must be at least 2 characters long")
                .MaximumLength(20).WithMessage("Code cannot exceed 20 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0");
        }
    }
}
