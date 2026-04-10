using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Invoice entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Invoice entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
