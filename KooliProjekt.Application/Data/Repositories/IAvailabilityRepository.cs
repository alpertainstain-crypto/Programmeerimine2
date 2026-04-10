using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IAvailabilityRepository
    {
        Task<Availability?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Availability>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Availability entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Availability entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
