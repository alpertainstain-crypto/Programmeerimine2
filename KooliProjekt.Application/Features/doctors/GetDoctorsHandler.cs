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
    public class GetDoctorsHandler : IRequestHandler<GetDoctors, OperationResult<PagedResult<Doctor>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetDoctorsHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Doctor>>> Handle(GetDoctors request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Doctor>>();

            var query = _dbContext.Doctors.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(request.SearchFirstName))
            {
                query = query.Where(x => x.FirstName.Contains(request.SearchFirstName));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchLastName))
            {
                query = query.Where(x => x.LastName.Contains(request.SearchLastName));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchSpecialty))
            {
                query = query.Where(x => x.Specialty.Contains(request.SearchSpecialty));
            }

            result.Value = await query
                .OrderBy(x => x.DoctorId)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
