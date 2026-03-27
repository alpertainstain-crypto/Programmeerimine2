using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class SaveInvoiceLinesCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
