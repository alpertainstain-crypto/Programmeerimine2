using FluentValidation;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class SaveInvoiceLinesCommandValidator : AbstractValidator<SaveInvoiceLinesCommand>
    {
        public SaveInvoiceLinesCommandValidator()
        {
            RuleFor(x => x.InvoiceId)
                .GreaterThan(0).WithMessage("Invoice ID must be greater than 0");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}
