using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Appointment entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Appointment entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
