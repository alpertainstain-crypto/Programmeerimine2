using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class SaveVisitDocumentCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string FilePath { get; set; } = default!;
        public string FileType { get; set; } = default!;
        public int UploadedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
