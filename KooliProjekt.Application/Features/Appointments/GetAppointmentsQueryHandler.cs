using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features
{
    public class GetAppointmentsQueryHandler : IRequestHandler<AppointmentsQuery, OperationResult<PagedResult<Appointment>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAppointmentsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Appointment>>> Handle(AppointmentsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Appointment>>();

            var query = _dbContext.Appointments.AsQueryable();

            // Apply search filters
            if (request.SearchFromDate.HasValue)
            {
                query = query.Where(x => x.Time >= request.SearchFromDate.Value);
            }

            if (request.SearchToDate.HasValue)
            {
                query = query.Where(x => x.Time <= request.SearchToDate.Value);
            }

            if (request.SearchDoctorId.HasValue && request.SearchDoctorId > 0)
            {
                query = query.Where(x => x.DoctorId == request.SearchDoctorId.Value);
            }

            if (request.SearchUserId.HasValue && request.SearchUserId > 0)
            {
                query = query.Where(x => x.UserId == request.SearchUserId.Value);
            }

            result.Value = await query
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
