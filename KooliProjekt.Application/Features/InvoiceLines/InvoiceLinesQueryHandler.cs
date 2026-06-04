using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class InvoiceLinesQueryHandler : IRequestHandler<InvoiceLinesQuery, OperationResult<PagedResult<InvoiceLine>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceLine>>> Handle(InvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<InvoiceLine>>();

            var query = _dbContext.InvoiceLines.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(request.SearchDescription))
            {
                query = query.Where(x => x.Description.Contains(request.SearchDescription));
            }

            if (request.SearchInvoiceId.HasValue && request.SearchInvoiceId > 0)
            {
                query = query.Where(x => x.InvoiceId == request.SearchInvoiceId.Value);
            }

            result.Value = await query
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
