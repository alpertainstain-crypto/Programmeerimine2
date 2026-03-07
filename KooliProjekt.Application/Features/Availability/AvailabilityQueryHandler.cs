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
                .GetPagedAsync(
                    ((KooliProjekt.Application.Features.GetAvailability)request).Page,
                    ((KooliProjekt.Application.Features.GetAvailability)request).PageSize
                );

            return result;
        }
    }
}
