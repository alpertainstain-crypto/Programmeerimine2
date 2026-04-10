using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IAdminOverrideRepository
    {
        Task<AdminOverride?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<AdminOverride>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(AdminOverride entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(AdminOverride entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
