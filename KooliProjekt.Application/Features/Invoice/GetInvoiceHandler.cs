using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetInvoiceHandler : IRequestHandler<GetInvoice, OperationResult<PagedResult<Invoice>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Invoice>>> Handle(GetInvoice request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Invoice>>();

            result.Value = await _dbContext
                .Invoice
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
