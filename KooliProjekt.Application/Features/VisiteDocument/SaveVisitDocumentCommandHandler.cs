using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class SaveVisitDocumentCommandHandler : IRequestHandler<SaveVisitDocumentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveVisitDocumentCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveVisitDocumentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var visitDocument = new VisitDocument();
            if (request.Id == 0)
            {
                visitDocument.CreatedAt = DateTime.Now;
                await _dbContext.VisitDocuments.AddAsync(visitDocument, cancellationToken);
            }
            else
            {
                visitDocument = await _dbContext.VisitDocuments.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
                if (visitDocument == null)
                {
                    result.AddError("Visit document not found");
                    return result;
                }
            }

            visitDocument.AppointmentId = request.AppointmentId;
            visitDocument.FileType = request.FileType;
            visitDocument.UploadedBy = request.UploadedBy;
            visitDocument.CreatedAt = request.CreatedAt;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
