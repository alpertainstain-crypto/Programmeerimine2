using System.Collections.Generic;
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
    public class DoctorsQueryHandler : IRequestHandler<DoctorsQuery, OperationResult<PagedResult<Doctor>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Doctor>>> Handle(DoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Doctor>>();

            result.Value = await _dbContext
                .Doctors 
                .OrderBy(list => list.Time)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}