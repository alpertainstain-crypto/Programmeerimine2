using FluentValidation;

namespace KooliProjekt.Application.Features.AdminOverrideList
{
    public class SaveAdminOverrideCommandValidator : AbstractValidator<SaveAdminOverrideCommand>
    {
        public SaveAdminOverrideCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters long")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Start)
                .NotNull().WithMessage("Start date is required");

            RuleFor(x => x.End)
                .NotNull().WithMessage("End date is required")
                .GreaterThan(x => x.Start).WithMessage("End date must be after start date");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0).WithMessage("Doctor ID must be greater than 0");

            RuleFor(x => x.CreatedBy)
                .GreaterThan(0).WithMessage("Created by (User ID) must be greater than 0");
        }
    }
}
