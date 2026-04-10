using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Doctor entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Doctor entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
