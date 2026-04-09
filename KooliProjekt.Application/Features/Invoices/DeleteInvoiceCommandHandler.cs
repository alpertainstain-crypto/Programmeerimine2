using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteInvoiceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var invoice = await _dbContext.Invoice.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (invoice == null)
            {
                result.AddError("Invoice not found");
                return result;
            }

            _dbContext.Invoice.Remove(invoice);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
