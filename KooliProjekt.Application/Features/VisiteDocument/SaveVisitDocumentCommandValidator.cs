using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class SaveVisitDocumentCommandValidator : AbstractValidator<SaveVisitDocumentCommand>
    {
        public SaveVisitDocumentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .GreaterThan(0).WithMessage("Appointment ID must be greater than 0");

            RuleFor(x => x.FilePath)
                .NotEmpty().WithMessage("File path is required")
                .MinimumLength(3).WithMessage("File path must be at least 3 characters long")
                .MaximumLength(260).WithMessage("File path cannot exceed 260 characters");

            RuleFor(x => x.FileType)
                .NotEmpty().WithMessage("File type is required")
                .Must(x => new[] { "pdf", "doc", "docx", "jpg", "png", "txt" }.Contains(x.ToLower()))
                .WithMessage("File type must be one of: pdf, doc, docx, jpg, png, txt");

            RuleFor(x => x.UploadedBy)
                .GreaterThan(0).WithMessage("Uploaded by (User ID) must be greater than 0");
        }
    }
}
