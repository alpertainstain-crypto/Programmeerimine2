using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLinesHandler : IRequestHandler<GetInvoiceLines, OperationResult<PagedResult<InvoiceLine>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceLinesHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceLine>>> Handle(GetInvoiceLines request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<InvoiceLine>>();

            result.Value = await _dbContext
                .InvoiceLines
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
