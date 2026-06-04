using FluentValidation;

namespace KooliProjekt.Application.Features.doctors
{
    public class SaveDoctorsCommandValidator : AbstractValidator<SaveDoctorsCommand>
    {
        public SaveDoctorsCommandValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters long")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
        }
    }
}
