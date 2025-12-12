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
    public class GetAppointmentsHandler : IRequestHandler<GetAppointments, OperationResult<PagedResult<Appointment>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAppointmentsHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Appointment>>> Handle(GetAppointments request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Appointment>>();

            result.Value = await _dbContext
                .Appointments
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
