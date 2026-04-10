using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Service>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Service entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Service entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
