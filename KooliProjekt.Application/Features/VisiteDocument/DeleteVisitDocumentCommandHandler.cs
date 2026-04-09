using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class DeleteVisitDocumentCommandHandler : IRequestHandler<DeleteVisitDocumentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteVisitDocumentCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteVisitDocumentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var visitDocument = await _dbContext.VisitDocuments.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (visitDocument == null)
            {
                result.AddError("Visit document not found");
                return result;
            }

            _dbContext.VisitDocuments.Remove(visitDocument);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
