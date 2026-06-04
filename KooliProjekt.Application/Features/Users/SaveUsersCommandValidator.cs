using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUsersCommandValidator : AbstractValidator<SaveUsersCommand>
    {
        public SaveUsersCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^\+?[0-9\s\-()]{6,20}$").WithMessage("Phone must be a valid phone number format");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .Must(x => new[] { "Doctor", "Patient", "Admin", "Staff" }.Contains(x))
                .WithMessage("Role must be one of: Doctor, Patient, Admin, Staff");
        }
    }
}
