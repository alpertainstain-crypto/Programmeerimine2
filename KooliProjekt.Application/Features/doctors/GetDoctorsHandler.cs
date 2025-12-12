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

            result.Value = await _dbContext
                .Doctors
                .OrderBy(x => x.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
