using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IInvoiceLineRepository
    {
        Task<InvoiceLine?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<InvoiceLine>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(InvoiceLine entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(InvoiceLine entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
