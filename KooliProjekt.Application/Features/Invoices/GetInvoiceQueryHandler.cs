using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.invoice
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Invoice
                .Include(list => list.Lines)
                .Where(list => list.Id == request.Id)
                .Select(list => new
                {
                    Id = list.Id,
                    InvoiceNo = list.InvoiceNo,
                    InvoiceDate = list.InvoiceDate,
                    DueDate = list.DueDate,
                    Status = list.Status,
                    Lines = list.Lines.Select(item => new
                    {
                        item.Id,
                        item.Description,
                        item.Amount
                    })
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
