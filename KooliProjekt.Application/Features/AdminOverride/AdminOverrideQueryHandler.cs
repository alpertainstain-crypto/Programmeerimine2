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
    public class AdminOverrideQueryHandler : IRequestHandler<AdminOverrideQuery, OperationResult<PagedResult<AdminOverride>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminOverrideQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<AdminOverride>>> Handle(AdminOverrideQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<AdminOverride>>();

            result.Value = await _dbContext
                .AdminOverride
                .OrderBy(list => list.Title)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }

        public Task<OperationResult<PagedResult<AdminOverride>>> Handle(AdminOverrideQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
