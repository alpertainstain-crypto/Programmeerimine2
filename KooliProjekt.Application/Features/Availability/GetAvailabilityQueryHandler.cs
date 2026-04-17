using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetAvailabilityQueryHandler : IRequestHandler<AvailabilityQuery, OperationResult<PagedResult<global::Availability>>>
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public GetAvailabilityQueryHandler(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<OperationResult<PagedResult<global::Availability>>> Handle(AvailabilityQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<global::Availability>>();

            var availabilities = await _availabilityRepository.GetAllAsync(cancellationToken);
            var pagedResult = availabilities
                .OrderBy(x => x.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            result.Value = new PagedResult<global::Availability>
            {
                Results = pagedResult,
                RowCount = availabilities.Count,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };

            return result;
        }
    }
}