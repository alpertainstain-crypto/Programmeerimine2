using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class DeleteInvoiceLineCommandHandler : IRequestHandler<DeleteInvoiceLineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteInvoiceLineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var invoiceLine = await _dbContext.InvoiceLines.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (invoiceLine == null)
            {
                result.AddError("Invoice line not found");
                return result;
            }

            _dbContext.InvoiceLines.Remove(invoiceLine);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
