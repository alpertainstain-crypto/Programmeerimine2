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

            var query = _dbContext.Invoice.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(request.SearchStatus))
            {
                query = query.Where(x => x.Status == request.SearchStatus);
            }

            if (request.SearchFromDate.HasValue)
            {
                query = query.Where(x => x.InvoiceDate >= request.SearchFromDate.Value);
            }

            if (request.SearchToDate.HasValue)
            {
                query = query.Where(x => x.InvoiceDate <= request.SearchToDate.Value);
            }

            result.Value = await query
                .OrderBy(list => list.InvoiceDate)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}