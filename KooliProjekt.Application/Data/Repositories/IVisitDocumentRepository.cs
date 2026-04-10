using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IVisitDocumentRepository
    {
        Task<VisitDocument?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<VisitDocument>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(VisitDocument entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(VisitDocument entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
