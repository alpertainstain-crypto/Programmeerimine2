using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features
{
    public class InvoiceQueryHandler : IRequestHandler<InvoiceQuery, OperationResult<PagedResult<Invoice>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Invoice>>> Handle(InvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Invoice>>();

            result.Value = await _dbContext
                .Invoice
                .OrderBy(list => list.InvoiceDate)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}