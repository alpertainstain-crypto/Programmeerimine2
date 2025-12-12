using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetAvailability : IRequest<OperationResult<PagedResult<Availability>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
