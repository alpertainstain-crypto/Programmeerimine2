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
    public class GetAdminOverrideHandler : IRequestHandler<GetAdminOverride, OperationResult<PagedResult<AdminOverride>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAdminOverrideHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<AdminOverride>>> Handle(GetAdminOverride request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<AdminOverride>>();

            result.Value = await _dbContext
                .AdminOverride
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
