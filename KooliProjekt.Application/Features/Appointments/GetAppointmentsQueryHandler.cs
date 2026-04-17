using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetAppointmentsQueryHandler : IRequestHandler<AppointmentsQuery, OperationResult<PagedResult<Appointment>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<OperationResult<PagedResult<Appointment>>> Handle(AppointmentsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Appointment>>();

            var appointments = await _appointmentRepository.GetAllAsync(cancellationToken);
            var pagedResult = appointments
                .OrderBy(x => x.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            result.Value = new PagedResult<Appointment>
            {
                Results = pagedResult,
                RowCount = appointments.Count,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };

            return result;
        }
    }
}
