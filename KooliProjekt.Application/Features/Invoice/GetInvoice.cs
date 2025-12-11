using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoice
{
    public class GetInvoice : IRequest<OperationResult<PagedResult<Invoice>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
