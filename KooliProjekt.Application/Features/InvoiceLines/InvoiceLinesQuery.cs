using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class InvoiceLinesQuery : IRequest<OperationResult<PagedResult<InvoiceLine>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchDescription { get; set; }
        public int? SearchInvoiceId { get; set; }
    }
}
