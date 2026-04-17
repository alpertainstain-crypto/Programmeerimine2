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
    public class GetServicesHandler : IRequestHandler<GetServices, OperationResult<PagedResult<Service>>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<OperationResult<PagedResult<Service>>> Handle(GetServices request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Service>>();

            var services = await _serviceRepository.GetAllAsync(cancellationToken);
            var pagedResult = services
                .OrderBy(x => x.Code)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            result.Value = new PagedResult<Service>
            {
                Results = pagedResult,
                RowCount = services.Count,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };

            return result;
        }
    }
}
