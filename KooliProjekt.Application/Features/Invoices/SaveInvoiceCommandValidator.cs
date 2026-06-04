using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandValidator : AbstractValidator<SaveInvoiceCommand>
    {
        public SaveInvoiceCommandValidator()
        {
            RuleFor(x => x.InvoiceNo)
                .GreaterThan(0).WithMessage("Invoice number must be greater than 0");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(x => new[] { "Pending", "Paid", "Cancelled", "Overdue" }.Contains(x))
                .WithMessage("Status must be one of: Pending, Paid, Cancelled, Overdue");

            RuleFor(x => x.Subtotal)
                .GreaterThanOrEqualTo(0).WithMessage("Subtotal cannot be negative");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative")
                .LessThanOrEqualTo(x => x.Subtotal).WithMessage("Discount cannot exceed subtotal");

            RuleFor(x => x.GrandTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Grand total cannot be negative")
                .Equal(x => x.Subtotal - x.Discount).WithMessage("Grand total must equal subtotal minus discount");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(x => x.InvoiceDate).WithMessage("Due date must be after invoice date");

            RuleFor(x => x.AppointmentId)
                .GreaterThan(0).WithMessage("Appointment ID must be greater than 0");
        }
    }
}
