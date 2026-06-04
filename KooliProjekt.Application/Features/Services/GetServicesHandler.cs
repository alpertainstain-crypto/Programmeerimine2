using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetServicesHandler : IRequestHandler<GetServices, OperationResult<PagedResult<Service>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetServicesHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Service>>> Handle(GetServices request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Service>>();

            var query = _dbContext.Services.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(request.SearchCode))
            {
                query = query.Where(x => x.Code.Contains(request.SearchCode));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchDescription))
            {
                query = query.Where(x => x.Description.Contains(request.SearchDescription));
            }

            result.Value = await query
                .OrderBy(x => x.Code)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
