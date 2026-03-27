using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class SaveInvoiceLinesCommandHandler : IRequestHandler<SaveInvoiceLinesCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveInvoiceLinesCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveInvoiceLinesCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var invoiceLine = new InvoiceLine();
            if (request.Id == 0)
            {
                await _dbContext.InvoiceLines.AddAsync(invoiceLine, cancellationToken);
            }
            else
            {
                invoiceLine = await _dbContext.InvoiceLines.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
                if (invoiceLine == null)
                {
                    result.AddError("Invoice line not found");
                    return result;
                }
            }

            invoiceLine.InvoiceId = request.InvoiceId;
            invoiceLine.Description = request.Description;
            invoiceLine.Amount = request.Amount;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
