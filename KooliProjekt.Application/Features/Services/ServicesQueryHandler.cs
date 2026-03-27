using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class ServicesQueryHandler : IRequestHandler<ServicesQuery, OperationResult<PagedResult<Service>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ServicesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Service>>> Handle(ServicesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Service>>();

            result.Value = await _dbContext
                .Services
                .OrderBy(x => x.Code)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
