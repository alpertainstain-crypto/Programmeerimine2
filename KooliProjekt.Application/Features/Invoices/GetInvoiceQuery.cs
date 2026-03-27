using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.invoice
{
    public class GetInvoiceQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
        public int Page { get; internal set; }
        public int PageSize { get; internal set; }
    }
}