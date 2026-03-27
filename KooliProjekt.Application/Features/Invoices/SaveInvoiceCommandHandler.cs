using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandHandler : IRequestHandler<SaveInvoiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveInvoiceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveInvoiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var invoice = new Invoice();
            if (request.Id == 0)
            {
                await _dbContext.Invoice.AddAsync(invoice, cancellationToken);
            }
            else
            {
                invoice = await _dbContext.Invoice.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
                if (invoice == null)
                {
                    result.AddError("Invoice not found");
                    return result;
                }
            }

            invoice.InvoiceNo = request.InvoiceNo;
            invoice.InvoiceDate = request.InvoiceDate;
            invoice.DueDate = request.DueDate;
            invoice.Status = request.Status;
            invoice.Subtotal = request.Subtotal;
            invoice.Discount = request.Discount;
            invoice.GrandTotal = request.GrandTotal;
            invoice.MarkedPaidAt = request.MarkedPaidAt;
            invoice.AppointmentId = request.AppointmentId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
