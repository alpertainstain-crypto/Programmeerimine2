using FluentValidation;

namespace KooliProjekt.Application.Features.Appointments
{
    public class SaveAppointmentsCommandValidator : AbstractValidator<SaveAppointmentsCommand>
    {
        public SaveAppointmentsCommandValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters long")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.AppointmentTime)
                .NotNull().WithMessage("Appointment time is required")
                .GreaterThan(System.DateTime.Now).WithMessage("Appointment time must be in the future");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than 0");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0).WithMessage("Doctor ID must be greater than 0");
        }
    }
}
