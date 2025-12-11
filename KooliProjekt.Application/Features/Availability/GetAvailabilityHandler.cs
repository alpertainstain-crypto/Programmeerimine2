using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Availability
{
    public class GetAvailabilityHandler : IRequestHandler<GetAvailability, OperationResult<PagedResult<Availability>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAvailabilityHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Availability>>> Handle(GetAvailability request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Availability>>();

            result.Value = await _dbContext
                .Availability
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
